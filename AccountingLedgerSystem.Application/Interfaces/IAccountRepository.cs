using AccountingLedgerSystem.Domain.Entities;

namespace AccountingLedgerSystem.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccountsAsync();
        Task AddAccountAsync(Account account);
    }
}
