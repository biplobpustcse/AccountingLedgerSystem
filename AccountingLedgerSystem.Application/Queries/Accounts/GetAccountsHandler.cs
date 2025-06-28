using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccountingLedgerSystem.Application.Queries.Accounts;

public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, IEnumerable<AccountDto>>
{
    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;

    public GetAccountsHandler(IAccountRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AccountDto>> Handle(
        GetAccountsQuery request,
        CancellationToken cancellationToken
    )
    {
        var accounts = await _repository.GetAccountsAsync();
        return _mapper.Map<IEnumerable<AccountDto>>(accounts);
    }
}
