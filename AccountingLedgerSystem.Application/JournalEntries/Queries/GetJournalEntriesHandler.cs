using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using MediatR;

namespace AccountingLedgerSystem.Application.JournalEntries.Queries;

public class GetJournalEntriesHandler
    : IRequestHandler<GetJournalEntriesQuery, IEnumerable<JournalEntry>>
{
    private readonly IJournalEntryRepository _repository;

    public GetJournalEntriesHandler(IJournalEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<JournalEntry>> Handle(
        GetJournalEntriesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repository.GetJournalEntriesAsync();
    }
}
