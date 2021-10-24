using Application.DataTransfer.Packages;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Packages
{
    public class PackageValidator : AbstractValidator<PackageDto>
    {
        public PackageValidator(Context _context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Naziv peketa je obaveyan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Name)
                    .MinimumLength(3)
                    .MaximumLength(30)
                    .WithMessage("Naziv paketa nije u dobrom formatu");
                });
            RuleFor(x => x.Description).NotEmpty().WithMessage("Opis peketa je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Description)
                    .MinimumLength(3)
                    .MaximumLength(330)
                    .WithMessage("Opis paketa nije u dobrom formatu");
                });
            RuleFor(x => x.AccountMaintenance).NotEmpty()
                .WithMessage("Naknada za održavanje računa je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.AccountMaintenance).Must(x => x >= 0)
                    .WithMessage("Naknada za održavanje računa ne može biti negativan broj");
                });
        }
    }
}
