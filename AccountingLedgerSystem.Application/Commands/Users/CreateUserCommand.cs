using MediatR;

namespace AccountingLedgerSystem.Application.Commands.Users;

public class CreateUserCommand : IRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
