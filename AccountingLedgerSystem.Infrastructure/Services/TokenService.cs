using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AccountingLedgerSystem.Application.Helpers;
using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AccountingLedgerSystem.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository userRepository;

    public TokenService(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        this.userRepository = userRepository;
    }

    public async Task<(string AccessToken, string RefreshToken)> AuthenticateAsync(
        string email,
        string password
    )
    {
        var passwordHash = PasswordHelper.GeneratePasswordHash(password);
        var user = await userRepository.GetUserAsync(email, passwordHash);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials.");

        var accessToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshJwtToken(user);

        return (accessToken, refreshToken);
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(
        string refreshToken
    )
    {
        var handler = new JwtSecurityTokenHandler();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!)
            ),
            ValidateIssuer = true,
            ValidIssuer = _config["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = _config["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };

        try
        {
            var principal = handler.ValidateToken(
                refreshToken,
                tokenValidationParameters,
                out var validatedToken
            );

            var tokenType = principal.FindFirst("token_type")?.Value;
            if (tokenType != "refresh")
                throw new SecurityTokenException("Invalid token type.");

            var email = principal.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
                throw new SecurityTokenException("Invalid token.");

            var user = await userRepository.GetUserByEmailAsync(email); // Lightweight DB hit, optional

            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            var newAccessToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshJwtToken(user);

            return (newAccessToken, newRefreshToken);
        }
        catch (Exception)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token.");
        }
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[] { new Claim(ClaimTypes.Email, user.Email) };

        // Default to 60 minutes if not set
        var duration = _config["Jwt:DurationInMinutes"];
        var expires = DateTime.UtcNow.AddMinutes(
            string.IsNullOrEmpty(duration) ? 60 : double.Parse(duration)
        );

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("token_type", "refresh"),
        };

        var refreshJwtTokenExpires = GetRefreshTokenExpiry();

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: refreshJwtTokenExpires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private int GetRefreshTokenLifetimeDays()
    {
        return int.TryParse(_config["Jwt:RefreshTokenDays"], out var days) ? days : 7;
    }

    private DateTime GetRefreshTokenExpiry()
    {
        return DateTime.UtcNow.AddDays(GetRefreshTokenLifetimeDays());
    }
}
