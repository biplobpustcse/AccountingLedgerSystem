using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using MediatR;

namespace AccountingLedgerSystem.Application.Queries.Accounts;

public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, IEnumerable<Account>>
{
    private readonly IAccountRepository _repository;

    public GetAccountsHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Account>> Handle(
        GetAccountsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repository.GetAccountsAsync();
    }
}
