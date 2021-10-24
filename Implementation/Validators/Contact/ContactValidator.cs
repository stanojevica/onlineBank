using Application.DataTransfer.Email;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Contact
{
    public class ContactValidator : AbstractValidator<EmailDto>
    {
        public ContactValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email je obaveyan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email).EmailAddress()
                    .WithMessage("Email nije u dobrom formatu");
                });
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ime je obavezan parametar");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Naslov je obaveyan parametar");
            RuleFor(x => x.Message).NotEmpty().WithMessage("Poruka je obaveyan paraetar");
        }
    }
}
