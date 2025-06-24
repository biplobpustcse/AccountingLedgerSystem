namespace AccountingLedgerSystem.Application.DTOs;

public class JournalEntryLineDto
{
    public int Id { get; set; }

    public int JournalEntryId { get; set; }

    public int AccountId { get; set; }
    public string AccountName { get; set; } = default!;
    public string AccountType { get; set; } = default!;

    public decimal Debit { get; set; }

    public decimal Credit { get; set; }
}
