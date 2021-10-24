using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users
{
    public class CreateUserValidator : UserValidator
    {
        public CreateUserValidator(Context _context) : base(_context)
        {
            RuleFor(x => x.Email).Must(x => !_context.Users.Any(u => u.Email == x))
                .WithMessage("Korisnik sa tim emailom već postoji");
            RuleFor(x => x.IdentificationNumber).Must(x => !_context.Users.Any(u => u.IdentityNumber == x))
                .WithMessage("Korisnik sa tim identifikacionim brojem već postoji");
        }
    }
}
