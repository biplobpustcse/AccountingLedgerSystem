using System.Data;
using AccountingLedgerSystem.Application.Helpers;
using AccountingLedgerSystem.Persistence.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingLedgerSystem.Infrastructure.DbInitializer;

public static class DataSeeder
{
    public static void SeedInitialData(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var connection = db.Database.GetDbConnection();

        connection.Open();

        // Check if Accounts table already has data
        using (var checkCmd = connection.CreateCommand())
        {
            checkCmd.CommandText = "SELECT COUNT(*) FROM Accounts";
            var count = (int)(checkCmd.ExecuteScalar() ?? 0);
            if (count > 0)
                return; // already seeded
        }

        // Insert sample accounts
        var accounts = new[]
        {
            new { Name = "Cash", Type = "Asset" },
            new { Name = "Revenue", Type = "Revenue" },
            new { Name = "Expense", Type = "Expense" },
            new { Name = "Accounts Payable", Type = "Liability" },
        };

        foreach (var acc in accounts)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "sp_AddAccount";
            cmd.CommandType = CommandType.StoredProcedure;

            var nameParam = new SqlParameter("@Name", acc.Name);
            var typeParam = new SqlParameter("@Type", acc.Type);

            cmd.Parameters.Add(nameParam);
            cmd.Parameters.Add(typeParam);

            cmd.ExecuteNonQuery();
        }

        // Sample journal entry: Debit Cash, Credit Revenue
        int journalEntryId;

        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = "sp_AddJournalEntry";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@Date", DateTime.UtcNow));
            cmd.Parameters.Add(new SqlParameter("@Description", "Initial Sales"));

            var outputId = new SqlParameter("@NewJournalEntryId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
            };
            cmd.Parameters.Add(outputId);

            cmd.ExecuteNonQuery();
            journalEntryId = (int)outputId.Value;
        }

        // Add two lines
        var lines = new[]
        {
            new
            {
                AccountId = 1,
                Debit = 1000m,
                Credit = 0m,
            }, // Cash
            new
            {
                AccountId = 2,
                Debit = 0m,
                Credit = 1000m,
            }, // Revenue
        };

        foreach (var line in lines)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "sp_AddJournalEntryLine";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@JournalEntryId", journalEntryId));
            cmd.Parameters.Add(new SqlParameter("@AccountId", line.AccountId));
            cmd.Parameters.Add(new SqlParameter("@Debit", line.Debit));
            cmd.Parameters.Add(new SqlParameter("@Credit", line.Credit));

            cmd.ExecuteNonQuery();
        }

        // Check if Users table already has data
        using (var checkUserCmd = connection.CreateCommand())
        {
            checkUserCmd.CommandText = "SELECT COUNT(*) FROM Users";
            var count = (int)(checkUserCmd.ExecuteScalar() ?? 0);
            if (count > 0)
                return; // ✅ already seeded
        }

        // Create a default user
        var email = "admin@example.com";
        var password = "admin123";

        // Hash the password (must match your real hashing logic)
        var passwordHash = PasswordHelper.GeneratePasswordHash(password);

        // Insert user via stored procedure
        using (var userCmd = connection.CreateCommand())
        {
            userCmd.CommandText = "sp_AddUsers";
            userCmd.CommandType = CommandType.StoredProcedure;

            userCmd.Parameters.Add(new SqlParameter("@Email", email));
            userCmd.Parameters.Add(new SqlParameter("@PasswordHash", passwordHash));

            userCmd.ExecuteNonQuery();
        }
    }
}
