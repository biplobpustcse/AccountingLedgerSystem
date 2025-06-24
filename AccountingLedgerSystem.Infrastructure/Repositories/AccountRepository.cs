using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using AccountingLedgerSystem.Persistence.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedgerSystem.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
        return await _context.Accounts.FromSqlRaw("EXEC sp_GetAccounts").ToListAsync();
    }

    public async Task AddAccountAsync(Account account)
    {
        var conn = _context.Database.GetDbConnection();

        await using var command = conn.CreateCommand();
        command.CommandText = "sp_AddAccount";
        command.CommandType = System.Data.CommandType.StoredProcedure;

        var parameters = new[]
        {
            new SqlParameter("@Name", account.Name),
            new SqlParameter("@Type", account.Type),
        };
        command.Parameters.AddRange(parameters);

        if (conn.State != System.Data.ConnectionState.Open)
            await conn.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }
}
