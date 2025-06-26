using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Domain.Entities;
using MediatR;

namespace AccountingLedgerSystem.Application.JournalEntries.Queries;

public class GetJournalEntriesQuery : IRequest<IEnumerable<JournalEntryDto>> { }
