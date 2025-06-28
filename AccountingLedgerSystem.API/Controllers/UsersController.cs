using AccountingLedgerSystem.Application.Commands.Users;
using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AccountingLedgerSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ITokenService tokenService;

    public UsersController(IMediator mediator, ITokenService tokenService)
    {
        _mediator = mediator;
        this.tokenService = tokenService;
    }

    [HttpPost("Registration")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserDto loginDto)
    {
        try
        {
            var token = await tokenService.AuthenticateAsync(loginDto.Email, loginDto.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetUsersQuery());
        return Ok(result);
    }
}
