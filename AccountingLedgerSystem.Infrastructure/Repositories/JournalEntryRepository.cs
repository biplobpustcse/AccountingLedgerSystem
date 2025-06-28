using AccountingLedgerSystem.Application.Commands.Journal;
using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using AccountingLedgerSystem.Persistence.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedgerSystem.Infrastructure.Repositories;

public class JournalEntryRepository : IJournalEntryRepository
{
    private readonly ApplicationDbContext _context;

    public JournalEntryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddJournalEntryAsync(CreateJournalEntryCommand request)
    {
        var connection = _context.Database.GetDbConnection();
        await connection.OpenAsync();

        using var transaction = connection.BeginTransaction();

        try
        {
            // Insert JournalEntry (header)
            var insertCmd = connection.CreateCommand();
            insertCmd.Transaction = transaction;
            insertCmd.CommandText = "sp_AddJournalEntry";
            insertCmd.CommandType = System.Data.CommandType.StoredProcedure;

            insertCmd.Parameters.Add(new SqlParameter("@Date", request.Date));
            insertCmd.Parameters.Add(new SqlParameter("@Description", request.Description));

            var newId = new SqlParameter("@NewJournalEntryId", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output,
            };
            insertCmd.Parameters.Add(newId);

            await insertCmd.ExecuteNonQueryAsync();
            var journalEntryId = (int)newId.Value;

            // Insert each line
            foreach (var line in request.Lines)
            {
                var lineCmd = connection.CreateCommand();
                lineCmd.Transaction = transaction;
                lineCmd.CommandText = "sp_AddJournalEntryLine";
                lineCmd.CommandType = System.Data.CommandType.StoredProcedure;

                lineCmd.Parameters.Add(new SqlParameter("@JournalEntryId", journalEntryId));
                lineCmd.Parameters.Add(new SqlParameter("@AccountId", line.AccountId));
                lineCmd.Parameters.Add(new SqlParameter("@Debit", line.Debit));
                lineCmd.Parameters.Add(new SqlParameter("@Credit", line.Credit));

                await lineCmd.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync()
    {
        return await _context
            .JournalEntries.Include(e => e.Lines)
            .ThenInclude(l => l.Account)
            .ToListAsync();
    }

    public async Task<IEnumerable<TrialBalanceDto>> GetTrialBalanceAsync()
    {
        return await _context
            .TrialBalanceResults.FromSqlRaw("EXEC sp_GetTrialBalance")
            .ToListAsync();
    }
}
