using Application.DataTransfer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Commands.UsersCommands
{
    public interface ICreateUserCommand : ICommand<UserDto>
    {
    }
}
