using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Application.Interfaces;
using MediatR;

namespace AccountingLedgerSystem.Application.Queries.Users;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _repository;

    public GetUsersHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        var users = await _repository.GetAllAsync();
        return users.Select(u => new UserDto { Email = u.Email, Password = u.PasswordHash });
    }
}
