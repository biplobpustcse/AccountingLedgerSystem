using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Application.Interfaces;
using MediatR;

namespace AccountingLedgerSystem.Application.Queries.Journal;

public class GetTrialBalanceHandler
    : IRequestHandler<GetTrialBalanceQuery, IEnumerable<TrialBalanceDto>>
{
    private readonly IJournalEntryRepository _repository;

    public GetTrialBalanceHandler(IJournalEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TrialBalanceDto>> Handle(
        GetTrialBalanceQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repository.GetTrialBalanceAsync();
    }
}
