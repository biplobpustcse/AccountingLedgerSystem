using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Application.Interfaces;
using MediatR;

namespace AccountingLedgerSystem.Application.JournalEntries.Queries;

public class GetTrialBalanceHandler
    : IRequestHandler<GetTrialBalanceQuery, IEnumerable<TrialBalanceDto>>
{
    private readonly IJournalEntryRepository _repository;

    public GetTrialBalanceHandler(IJournalEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TrialBalanceDto>> Handle(
        GetTrialBalanceQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repository.GetTrialBalanceAsync();
    }
}
