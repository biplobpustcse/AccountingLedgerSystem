
namespace AccountingLedgerSystem.Domain.Entities;

public class JournalEntryLine
{
    public int Id { get; set; }

    public int JournalEntryId { get; set; }
    public JournalEntry JournalEntry { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; }

    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}
