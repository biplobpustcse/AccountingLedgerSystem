using AccountingLedgerSystem.Application.DTOs;
using MediatR;

namespace AccountingLedgerSystem.Application.JournalEntries.Commands;

public class CreateJournalEntryCommand : IRequest
{
    public DateTime Date { get; set; }
    public string Description { get; set; } = default!;
    public List<JournalEntryLineDto> Lines { get; set; } = new();
}
