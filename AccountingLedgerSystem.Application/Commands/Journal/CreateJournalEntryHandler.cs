using AccountingLedgerSystem.Application.Interfaces;
using MediatR;

namespace AccountingLedgerSystem.Application.Commands.Journal;

public class CreateJournalEntryHandler : IRequestHandler<CreateJournalEntryCommand>
{
    private readonly IJournalEntryRepository _repository;

    public CreateJournalEntryHandler(IJournalEntryRepository journalEntryRepository)
    {
        _repository = journalEntryRepository;
    }

    public async Task<Unit> Handle(
        CreateJournalEntryCommand request,
        CancellationToken cancellationToken
    )
    {
        await _repository.AddJournalEntryAsync(request);
        return Unit.Value;
    }
}
