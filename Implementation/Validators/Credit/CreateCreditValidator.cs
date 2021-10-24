using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Credit
{
    public class CreateCreditValidator : CreditValidator
    {
        public CreateCreditValidator(Context _context) : base(_context)
        {
            RuleFor(x => x.Name).Must(x => !_context.Credits.Any(c => c.Name == x))
                .WithMessage("Kredit sa tim imenom vec postoji");
        }
    }
}
