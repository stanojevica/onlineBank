using Application.DataTransfer.Users.Transactions;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries
{
    public interface IGetTransactionQuery : IQuery<SearchTransaction, IEnumerable<TransactionDto>>
    {
    }
}
