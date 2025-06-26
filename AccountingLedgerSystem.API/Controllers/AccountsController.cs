using AccountingLedgerSystem.Application.Commands.Accounts;
using AccountingLedgerSystem.Application.Queries.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingLedgerSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAccountsQuery());
        return Ok(result);
    }
}
