namespace AccountingLedgerSystem.Application.Interfaces;

public interface ITokenService
{
    //Task<string> AuthenticateAsync(string email, string password);
    Task<(string AccessToken, string RefreshToken)> AuthenticateAsync(
        string email,
        string password
    );
    Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);
}
