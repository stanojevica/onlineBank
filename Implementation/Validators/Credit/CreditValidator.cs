using Application.DataTransfer.Credits;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Credit
{
    public class CreditValidator : AbstractValidator<CreditDto>
    {
        public CreditValidator(Context _context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ime je obavezan parametar");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Opis je obavezan parametar");
            RuleFor(x => x.CreditType).NotEmpty().WithMessage("Tip kredita je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.CreditType)
                    .Must(x => _context.CreditTypes.Any(t => t.Id == x))
                    .WithMessage("Tip kredita ne postoji");
                });
            RuleFor(x => x.CreditCalculations).NotEmpty().WithMessage("Kreditna kalkulacija je obavezan parametar").DependentRules(() =>
            {
                RuleFor(x => x.CreditCalculations).Must(c => c.Distinct().Count() == c.Count()).WithMessage("Duplikati nisu dozvoljeni")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.CreditCalculations).SetValidator(new CreditCalculationValidator());
                });
            });
            RuleFor(x => x.CreditConditions).NotEmpty().WithMessage("Uslovi za kredit su obavezan paraetar").DependentRules(() =>
            {
                RuleFor(x => x.CreditConditions).Must(c => c.Distinct().Count() == c.Count()).WithMessage("Duplikati nisu dozvoljeni")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.CreditConditions).SetValidator(new CreditConditionsValidator());
                });
            });
        }
    }

    public class CreditCalculationValidator : AbstractValidator<CreditCalculationDto>
    {
        public CreditCalculationValidator()
        {
            RuleFor(x => x.Interest).NotEmpty().WithMessage("Kamata je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Interest)
                    .GreaterThan(0)
                    .WithMessage("Iznos kamate ne moze biti manji od 0");
                });
            RuleFor(x => x.MinYear).NotEmpty().WithMessage("Minimalne godine su obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.MinYear)
                    .GreaterThan(0)
                    .LessThan(x => x.MaxYear)
                    .WithMessage("Minimalne godine ne mogu biti manje od 0 i vece od maksimalnih godina");
                });
            RuleFor(x => x.MaxYear).NotEmpty().WithMessage("Maksimalne godine su obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.MaxYear)
                    .GreaterThan(0)
                    .WithMessage("Maksimalne godine ne mogu biti manje od minimalnih godina");
                });
            RuleFor(x => x.MinAmout).NotEmpty().WithMessage("Minimalni iznos je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.MinAmout)
                    .GreaterThan(0)
                    .LessThan(x => x.MaxAmount)
                    .WithMessage("Minimalni iznos ne moze biti manji od 0 i veci od maksimalnog iznosa");
                });
            RuleFor(x => x.MaxAmount).NotEmpty().WithMessage("Maksimalan iznos je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.MaxAmount)
                    .GreaterThan(x => x.MinAmout)
                    .WithMessage("Maksimalan iznos ne moze biti manji od minimalnog iznosa");
                });
        }
    }
    public class CreditConditionsValidator : AbstractValidator<CreditConditionDto>
    {
        public CreditConditionsValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Naziv uslova je obavezan parametar");
            RuleFor(x => x.Condition).NotEmpty().WithMessage("Uslov je obavezan parametar");
        }
    }
}
