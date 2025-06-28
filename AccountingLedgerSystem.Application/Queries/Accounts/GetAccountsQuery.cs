using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingLedgerSystem.Application.DTOs;
using AccountingLedgerSystem.Domain.Entities;
using MediatR;

namespace AccountingLedgerSystem.Application.Queries.Accounts;

public class GetAccountsQuery : IRequest<IEnumerable<AccountDto>> { }
