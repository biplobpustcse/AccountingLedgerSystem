

namespace AccountingLedgerSystem.Domain.Entities;

public class JournalEntry
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = default!;

    public ICollection<JournalEntryLine> Lines { get; set; }
}
