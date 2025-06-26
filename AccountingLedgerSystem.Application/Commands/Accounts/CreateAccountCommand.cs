using MediatR;

namespace AccountingLedgerSystem.Application.Commands.Accounts;

public class CreateAccountCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;
}
