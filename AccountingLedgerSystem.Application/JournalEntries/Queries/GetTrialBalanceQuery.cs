using AccountingLedgerSystem.Application.DTOs;
using MediatR;

namespace AccountingLedgerSystem.Application.JournalEntries.Queries;

public class GetTrialBalanceQuery : IRequest<IEnumerable<TrialBalanceDto>> { }
