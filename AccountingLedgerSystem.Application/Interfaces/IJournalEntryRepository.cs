﻿using AccountingLedgerSystem.Application.Commands.Journal;
using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Domain.Entities;

namespace AccountingLedgerSystem.Application.Interfaces;

public interface IJournalEntryRepository
{
    Task AddJournalEntryAsync(CreateJournalEntryCommand request);
    Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync();
    Task<IEnumerable<TrialBalanceDto>> GetTrialBalanceAsync();
}
