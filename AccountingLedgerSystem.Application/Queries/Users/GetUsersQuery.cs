using AccountingLedgerSystem.Application.DTOs;
using MediatR;

namespace AccountingLedgerSystem.Application.Queries.Users;

public class GetUsersQuery : IRequest<IEnumerable<UserDto>> { }
