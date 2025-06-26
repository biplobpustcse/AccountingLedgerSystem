using AccountingLedgerSystem.Application.Accounts.Queries.GetAccounts;
using AccountingLedgerSystem.Application.JournalEntries.Commands;
using AccountingLedgerSystem.Application.JournalEntries.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingLedgerSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JournalEntriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public JournalEntriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("journalentries")]
    public async Task<IActionResult> Create([FromBody] CreateJournalEntryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("journalentries")]
    public async Task<IActionResult> GetAll()
    {
        var entries = await _mediator.Send(new GetJournalEntriesQuery());
        return Ok(entries);
    }

    [HttpGet("trialbalance")]
    public async Task<IActionResult> GetTrialBalance()
    {
        var result = await _mediator.Send(new GetTrialBalanceQuery());
        return Ok(result);
    }
}
