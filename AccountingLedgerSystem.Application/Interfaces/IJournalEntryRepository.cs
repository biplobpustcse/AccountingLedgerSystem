using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Application.JournalEntries.Commands;
using AccountingLedgerSystem.Domain.Entities;

namespace AccountingLedgerSystem.Application.Interfaces;

public interface IJournalEntryRepository
{
    Task AddJournalEntryAsync(CreateJournalEntryCommand request);
    Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync();
    Task<IEnumerable<TrialBalanceDto>> GetTrialBalanceAsync();
}
