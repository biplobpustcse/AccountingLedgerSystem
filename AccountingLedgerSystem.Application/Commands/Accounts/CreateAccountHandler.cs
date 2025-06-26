using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using MediatR;

namespace AccountingLedgerSystem.Application.Commands.Accounts;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand>
{
    private readonly IAccountRepository _repository;

    public CreateAccountHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(
        CreateAccountCommand request,
        CancellationToken cancellationToken
    )
    {
        var account = new Account { Name = request.Name, Type = request.Type };

        await _repository.AddAccountAsync(account);
        return Unit.Value;
    }
}
