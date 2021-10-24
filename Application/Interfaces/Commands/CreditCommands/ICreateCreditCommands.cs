using Application.DataTransfer.Credits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Commands.CreditCommands
{
    public interface ICreateCreditCommands : ICommand<CreditDto>
    {
    }
}
