using Application.DataTransfer.Users;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator(Context _context)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ime korisnika je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.FirstName)
                    .MinimumLength(3)
                    .MaximumLength(30)
                    .Matches("^[A-Z][a-z]")
                    .WithMessage("Ime korisnika nije u dobrom formatu.");
                });
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Prezime korisnika je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.LastName)
                    .MinimumLength(3)
                    .MaximumLength(30)
                    .Matches("^[A-Z][a-z]")
                    .WithMessage("Prezime korisnika nije u dobrom formatu.");
                });
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email korisnika je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email).EmailAddress()
                    .WithMessage("Email korisnika nije u dobrom formatu");
                });
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Uloga korisnika je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.RoleId).Must(x => _context.Roles.Any(r => r.Id == x))
                    .WithMessage("Uloga koju se izabrali ne postoji");
                });
            RuleFor(x => x.IdentificationNumber).NotEmpty().WithMessage("Korisnikov identifikacioni broj je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.IdentificationNumber)
                    .Matches(@"^\d+$")
                    .Must(x => x.Count() == 13)
                    .When(x => x.RoleId == 4)
                    .WithMessage("JMBG korisnika mora imati 13 cifara");
                    RuleFor(x => x.IdentificationNumber)
                    .Matches(@"^\d+$")
                    .Must(x => x.Count() == 13)
                    .When(x => x.RoleId == 2)
                    .WithMessage("JMBG bankara mora imati 13 cifara");
                    RuleFor(x => x.IdentificationNumber)
                    .Matches(@"^\d+$")
                    .Must(x => x.Count() == 6)
                    .When(x => x.RoleId == 5)
                    .WithMessage("PIB korisnika mora imati 13 cifara");
                });
        }
    }
}
