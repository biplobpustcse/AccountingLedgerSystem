using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using AccountingLedgerSystem.Persistence.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedgerSystem.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        var conn = _context.Database.GetDbConnection();

        await using var command = conn.CreateCommand();
        command.CommandText = "sp_AddUsers";
        command.CommandType = System.Data.CommandType.StoredProcedure;

        var parameters = new[]
        {
            new SqlParameter("@Email", user.Email),
            new SqlParameter("@PasswordHash", user.PasswordHash),
        };
        command.Parameters.AddRange(parameters);

        if (conn.State != System.Data.ConnectionState.Open)
            await conn.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.FromSqlRaw("EXEC sp_GetUsers").ToListAsync();
    }

    public async Task<User> GetUserAsync(string email, string password)
    {
        return await _context
            .Users.Where(x => x.Email == email && x.PasswordHash == password)
            .FirstOrDefaultAsync();
    }
}
