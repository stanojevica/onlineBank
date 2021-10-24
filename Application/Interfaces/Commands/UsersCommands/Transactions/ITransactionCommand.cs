using Application.DataTransfer.Users.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Commands.UsersCommands.Transactions
{
    public interface ITransactionCommand : ICommand<TransactionDto>
    {
    }
}
