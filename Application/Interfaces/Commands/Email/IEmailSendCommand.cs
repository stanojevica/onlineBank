using Application.DataTransfer.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Commands.Email
{
    public interface IEmailSendCommand : ICommand<EmailDto>
    {
    }
}
