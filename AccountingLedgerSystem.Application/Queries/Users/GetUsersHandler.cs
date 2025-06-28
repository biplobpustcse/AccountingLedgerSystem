using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AccountingLedgerSystem.Application.Queries.Users;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUsersHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        var users = await _repository.GetAllAsync();
        _mapper.Map<IEnumerable<UserDto>>(users);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
