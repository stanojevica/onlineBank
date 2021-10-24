using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Credit
{
    public class UpdateCreditValidator : CreditValidator
    {
        public UpdateCreditValidator(Context _context) : base(_context)
        {
            RuleFor(x => x.Name).Must((credit, x) => !_context.Credits.Any(c => c.Name == x && c.Id != credit.Id))
                .WithMessage("Kredit sa tim imenom vec postoji");
        }
    }
}
