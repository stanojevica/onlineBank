using Application.DataTransfer.Users.Credits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Commands.UsersCommands.Credits
{
    public interface IUpadteCreditStatusCommand : ICommand<CreditStatusDto>
    {
    }
}
