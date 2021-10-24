using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Packages
{
    public class CreatePackageValidator : PackageValidator
    {
        public CreatePackageValidator(Context _context) : base(_context)
        {
            RuleFor(x => x.Name).Must(x => !_context.Packages.Any(y => y.Name == x))
                .WithMessage("Paket sa tim imenom već postoji");

        }
    }
}
