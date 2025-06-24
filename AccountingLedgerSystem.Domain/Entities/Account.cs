
namespace AccountingLedgerSystem.Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!; // e.g., Asset, Liability, etc.
}
