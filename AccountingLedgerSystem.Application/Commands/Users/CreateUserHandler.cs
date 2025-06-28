using AccountingLedgerSystem.Application.Helpers;
using AccountingLedgerSystem.Application.Interfaces;
using AccountingLedgerSystem.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AccountingLedgerSystem.Application.Commands.Users;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<User>(request);
        await _repository.AddAsync(account);
        return Unit.Value;
    }
}
