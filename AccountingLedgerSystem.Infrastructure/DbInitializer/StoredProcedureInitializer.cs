using System.Data.Common;
using AccountingLedgerSystem.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountingLedgerSystem.Infrastructure.DbInitializer;

public static class StoredProcedureInitializer
{
    public static void EnsureStoredProcedures(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        DbConnection connection = db.Database.GetDbConnection();

        // Path to Infrastructure's output SqlScripts folder
        var basePath = AppContext.BaseDirectory;
        var sqlPath = Path.Combine(basePath, "SqlScripts");

        string[] procedureScripts = new[]
        {
            File.ReadAllText(Path.Combine(sqlPath, "sp_GetAccounts.sql")),
            File.ReadAllText(Path.Combine(sqlPath, "sp_AddAccount.sql")),
            File.ReadAllText(Path.Combine(sqlPath, "sp_AddJournalEntry.sql")),
            File.ReadAllText(Path.Combine(sqlPath, "sp_AddJournalEntryLine.sql")),
            File.ReadAllText(Path.Combine(sqlPath, "sp_GetTrialBalance.sql")),
            File.ReadAllText(Path.Combine(sqlPath, "sp_AddUsers.sql")),
            File.ReadAllText(Path.Combine(sqlPath, "sp_GetUsers.sql")),
        };

        connection.Open();
        foreach (var script in procedureScripts)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = script;
            cmd.ExecuteNonQuery();
        }
    }
}
