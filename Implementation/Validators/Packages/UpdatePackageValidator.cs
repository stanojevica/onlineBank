using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Packages
{
    public class UpdatePackageValidator : PackageValidator
    {
        public UpdatePackageValidator(Context _context) : base(_context)
        {
            RuleFor(x => x.Name).Must((package, name) => !_context.Packages.Any(y => y.Name == name && y.Id != package.Id))
                .WithMessage("Paket sa tim imenom već postoji");
        }
    }
}
