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
    public class RequestCreditValidator : AbstractValidator<CreditUserDto>
    {
        public RequestCreditValidator(Context _context)
        {
            RuleFor(x => x.CreditId).NotEmpty().WithMessage("Kredit je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.CreditId).Must(x => _context.Credits.Any(c => c.Id == x))
                    .WithMessage("Kredit koji ste izabrali ne postoji.");
                });
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Korisnik je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.UserId).Must(x => _context.Users.Any(u => u.Id == x))
                    .WithMessage("Korisnik kojeg ste izabrali ne postoji.");
                })
                .DependentRules(() =>
                {
                    RuleFor(x => x.UserId).Must(x => !_context.CreditUsers.Any(c => c.UserId == x))
                    .WithMessage("Korisnik već ima kredit i ne može aplicirati za drugi.");
                });
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Iznos je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Amount).Must(x => _context.CreditCalculations.Any(c => c.MinAmount < x)).WithMessage("Iznos ne može biti manji od minimalnog ponuđenog iznosa");
                    RuleFor(x => x.Amount).Must(x => _context.CreditCalculations.Any(c => c.MaxAmount > x)).WithMessage("Iznos ne može biti veći od maksimalnog ponuđenog iznosa");
                });
            RuleFor(x => x.Years).NotEmpty().WithMessage("Godine su obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Years).Must(x => _context.CreditCalculations.Any(c => c.MinYear < x)).WithMessage("Godine ne mogu biti manje od minimalnih ponuđenih godina");
                    RuleFor(x => x.Years).Must(x => _context.CreditCalculations.Any(c => c.MaxYear > x)).WithMessage("Godine ne mogu biti veće od maksimalnih ponuđenih godina");
                });

        }
    }
}
