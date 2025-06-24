using MediatR;

namespace AccountingLedgerSystem.Application.Accounts.Commands.CreateAccount;

public class CreateAccountCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;
}
