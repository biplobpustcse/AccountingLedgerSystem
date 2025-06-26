namespace AccountingLedgerSystem.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } // Use for login/registration
    public string PasswordHash { get; set; }
}
