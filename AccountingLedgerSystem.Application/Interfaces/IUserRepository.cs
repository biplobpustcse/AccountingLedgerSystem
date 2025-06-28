using AccountingLedgerSystem.Domain.Entities;

namespace AccountingLedgerSystem.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetUserByEmailAsync(string refreshToken);
    Task<User> GetUserAsync(string username, string password);
}
