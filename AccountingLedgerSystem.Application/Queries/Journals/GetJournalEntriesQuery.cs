using AccountingLedgerSystem.Application.DTOs;
using MediatR;

namespace AccountingLedgerSystem.Application.Queries.Journal;

public class GetJournalEntriesQuery : IRequest<IEnumerable<JournalEntryDto>> { }
