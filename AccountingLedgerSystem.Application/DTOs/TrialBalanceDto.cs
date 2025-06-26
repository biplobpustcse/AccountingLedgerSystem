namespace AccountingLedgerSystem.Application.DTOs;

public class TrialBalanceDto
{
    public int AccountId { get; set; }
    public string AccountName { get; set; } = string.Empty;
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}
