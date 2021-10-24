using Application.DataTransfer.Credits;
using Application.Exceptions;
using Application.Interfaces.Commands.CreditCommands;
using DataAccess;
using Domain.Etities;
using FluentValidation;
using Implementation.Validators.Credit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Credits
{
    public class UpdateCreditCommand : IUpdateCreditCommand
    {
        public int Id => 5;

        public string Name => "Izmena podataka o kreditu";

        private readonly Context _context;
        private readonly UpdateCreditValidator _validator;

        public UpdateCreditCommand(Context context, UpdateCreditValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(CreditDto request)
        {
            var credit = _context.Credits
                .Where(x => x.DeleteAt == null && x.Active == true)
                .FirstOrDefault(x => x.Id == request.Id);

            if (credit == null)
            {
                throw new EntityNotFoundException(typeof(Credit));
            }

            _validator.ValidateAndThrow(request);

            credit.Name = request.Name;
            credit.Description = request.Description;
            credit.TypeId = request.CreditType;
            credit.UpdateAt = DateTime.Now;

            var conditions = _context.CreditConditions
                .Where(x => x.CreditId == request.Id)
                .AsQueryable();
            foreach (var c in conditions)
            {
                var con = _context.CreditConditions.Find(c.Id);
                _context.CreditConditions.Remove(con);
            }
            foreach (var c in request.CreditConditions)
            {
                _context.CreditConditions.Add(new CreditCondition
                {
                    Name = c.Name,
                    Condition = c.Condition,
                    Credit = credit
                });
            }

            
            var calculations = _context.CreditCalculations
                .Where(x => x.CreditId == request.Id)
                .AsQueryable();
            foreach (var c in calculations)
            {
                var cal = _context.CreditCalculations.Find(c.Id);
                _context.CreditCalculations.Remove(cal);
                
            }
            foreach (var c in request.CreditCalculations)
            {
                _context.CreditCalculations.Add(new CreditCalculation
                {
                    MaxAmount = c.MaxAmount,
                    MinAmount = c.MinAmout,
                    MaxYear = c.MaxYear,
                    MinYear = c.MinYear,
                    Interest = c.Interest,
                    Credit = credit
                });
            }

            _context.SaveChanges();

        }
    }
}
