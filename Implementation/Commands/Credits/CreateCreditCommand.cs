using Application.DataTransfer.Credits;
using Application.Interfaces.Commands.CreditCommands;
using DataAccess;
using Domain.Etities;
using FluentValidation;
using Implementation.Validators.Credit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Credits
{
    public class CreateCreditCommand : ICreateCreditCommands
    {
        public int Id => 1;

        public string Name => "Kreiranje kredita";

        private readonly Context _context;
        private readonly CreateCreditValidator _validator;

        public CreateCreditCommand(Context context, CreateCreditValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(CreditDto request)
        {
            _validator.ValidateAndThrow(request);

            foreach(var c in request.CreditCalculations)
            {
                var brojac = 0;
                foreach(var cl in request.CreditCalculations)
                {
                    if(c.Interest == cl.Interest && c.MinAmout == cl.MinAmout && c.MaxAmount == cl.MaxAmount && c.MinYear == cl.MinYear && c.MaxYear == cl.MaxYear)
                    {
                        brojac++;
                    }
                }
                if(brojac > 1)
                {
                    throw new Exception("Duplikati nisu dozvoljeni.");
                }
            }

            var credit = new Credit
            {
                Name = request.Name,
                Description = request.Description,
                TypeId = request.CreditType,
                DeleteAt = null,
                Active = true,
                CreatedAt = DateTime.Now,
                UpdateAt = null
            };

            foreach (var c in request.CreditCalculations)
            {
                credit.CreditCalculations.Add(new CreditCalculation
                {
                    MaxAmount = c.MaxAmount,
                    MinAmount = c.MinAmout,
                    MaxYear = c.MaxYear,
                    MinYear = c.MinYear,
                    Interest = c.Interest
                });
            }

            foreach (var c in request.CreditConditions)
            {
                credit.CreditConditions.Add(new CreditCondition
                {
                    Name = c.Name,
                    Condition = c.Condition
                });
            }

            _context.Credits.Add(credit);
            _context.SaveChanges();
        }
    }
}
