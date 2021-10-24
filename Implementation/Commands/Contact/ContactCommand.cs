using Application.DataTransfer.Email;
using Application.Email;
using Application.Interfaces.Commands.Email;
using FluentValidation;
using Implementation.Validators.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Contact
{
    public class ContactCommand : IEmailSendCommand
    {
        public int Id => 33;

        public string Name => "Send email from contact form";

        private readonly ContactValidator _validator;
        private readonly IEmailSender _sender;

        public ContactCommand(ContactValidator validator, IEmailSender sender)
        {
            _validator = validator;
            _sender = sender;
        }

        public void Execute(EmailDto request)
        {
            _validator.ValidateAndThrow(request);
            _sender.Send(new EmailSenderDto
            {
                Subject = request.Subject,
                SendTo = "aspmoviesproject@gmail.com",
                Content = "Pošiljaoc: "+request.Name+" Sa mejla "+request.Email+"<br/>Poruka:<br/>"+request.Message
            });
        }
    }
}
