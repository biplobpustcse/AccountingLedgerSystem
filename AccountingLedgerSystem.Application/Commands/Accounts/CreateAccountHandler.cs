using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccountingLedgerSystem.Application.Commands.Accounts;

public class CreateAccountHandler : IRequestHandler<CreateAccountCommand>
{
    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;

    public CreateAccountHandler(IAccountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        CreateAccountCommand request,
        CancellationToken cancellationToken
    )
    {
        var account = _mapper.Map<Account>(request);
        await _repository.AddAccountAsync(account);
        return Unit.Value;
    }
}
