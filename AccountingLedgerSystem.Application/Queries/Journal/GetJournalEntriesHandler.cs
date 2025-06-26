using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AccountingLedgerSystem.Application.Queries.Journal;

public class GetJournalEntriesHandler
    : IRequestHandler<GetJournalEntriesQuery, IEnumerable<JournalEntryDto>>
{
    private readonly IJournalEntryRepository _repository;
    private readonly IMapper _mapper;

    public GetJournalEntriesHandler(IJournalEntryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<JournalEntryDto>> Handle(
        GetJournalEntriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var journalEntries = await _repository.GetJournalEntriesAsync();
        return _mapper.Map<IEnumerable<JournalEntryDto>>(journalEntries);
    }
}
