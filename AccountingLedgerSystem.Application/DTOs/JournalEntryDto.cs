namespace AccountingLedgerSystem.Application.DTOs;

public class JournalEntryDto
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; } = default!;

    public List<JournalEntryLineDto> Lines { get; set; } = new();
}
