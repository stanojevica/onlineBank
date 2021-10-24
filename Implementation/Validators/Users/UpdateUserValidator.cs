using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users
{
    public class UpdateUserValidator : UserValidator
    {
        public UpdateUserValidator(Context _context) : base(_context)
        {
            RuleFor(x => x.Email).Must((user, email) => !_context.Users.Any(u => u.Email == email && u.Id != user.Id))
                .WithMessage("Korisnik sa tim emailom već postoji");
            RuleFor(x => x.IdentificationNumber).Must((user, jmbg) => !_context.Users.Any(u => u.IdentityNumber == jmbg && u.Id != user.Id))
                .WithMessage("Korisnik sa tim JMBG-om već postoji");
        }
    }
}
