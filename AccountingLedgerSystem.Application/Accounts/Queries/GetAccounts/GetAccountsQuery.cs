using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingLedgerSystem.Domain.Entities;
using MediatR;

namespace AccountingLedgerSystem.Application.Accounts.Queries.GetAccounts;

public class GetAccountsQuery : IRequest<IEnumerable<Account>> { }
