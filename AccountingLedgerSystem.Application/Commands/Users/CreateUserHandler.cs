using AccountingLedgerSystem.Application.Helpers;
using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using MediatR;

namespace AccountingLedgerSystem.Application.Commands.Users;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var account = new User
        {
            Email = request.Email,
            PasswordHash = PasswordHelper.GeneratePasswordHash(request.Password),
        };

        await _repository.AddAsync(account);
        return Unit.Value;
    }
}
