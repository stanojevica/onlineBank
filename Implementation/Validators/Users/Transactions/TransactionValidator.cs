using Application.DataTransfer.Users.Transactions;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators.Users.Transactions
{
    public class TransactionValidator : AbstractValidator<TransactionDto>
    {
        public TransactionValidator(Context _context)
        {
            RuleFor(x => x.RecipientAccountNumber).NotEmpty().WithMessage("Primalac je obavezan prarametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.RecipientAccountNumber)
                    .Matches("^([0-9]{4}\\-){3}[0-9]{4}$")
                    .WithMessage("Broj računa nije u dobrom formatu.");
                })
                .DependentRules(() =>
                {
                    RuleFor(x => x.RecipientAccountNumber).Must(x => _context.Accounts.Any(s => s.AccountNumber == x))
                    .WithMessage("Primalac ne postoji u bazi");
                });
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Iznos je obavezan parametar")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Amount)
                    .GreaterThan(0)
                    .WithMessage("Iznos ne može biti manji od 0");
                });
            RuleFor(x => x.Purpose).NotEmpty().WithMessage("Svrha plaćanja je ibavezan parametar");
        }
    }
}
