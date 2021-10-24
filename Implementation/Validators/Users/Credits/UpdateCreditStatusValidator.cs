using Application.DataTransfer.Users.Credits;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users.Credits
{
    public class UpdateCreditStatusValidator : AbstractValidator<CreditStatusDto>
    {
        public UpdateCreditStatusValidator(Context _context)
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Korisnik je obavezan paraetar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.UserId).Must((x, user) => _context.CreditUsers.Any(c => c.UserId == user))
                    .WithMessage("Korisnik sa tim kreditom ne postoji u bazi.");
                });
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status kredita je obavezan parametar");
        }
    }
}
