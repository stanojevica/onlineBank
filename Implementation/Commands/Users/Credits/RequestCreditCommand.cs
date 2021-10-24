using Application.DataTransfer.Users.Credits;
using Application.Interfaces;
using Application.Interfaces.Commands.UsersCommands.Credits;
using DataAccess;
using Domain.Etities;
using FluentValidation;
using Implementation.Validators.Users.Credits;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Users.Credits
{
    public class RequestCreditCommand : IRequestCreditCommand
    {
        public int Id => 16;

        public string Name => "Request credit";

        private readonly Context _context;
        private readonly IApplicationUser _user;
        private readonly RequestCreditValidator _validator;

        public RequestCreditCommand(Context context, IApplicationUser user, RequestCreditValidator validator)
        {
            _context = context;
            _user = user;
            _validator = validator;
        }

        public void Execute(CreditUserDto request)
        {
            var user = _context.Users.Find(_user.Id);
            if (user.RoleId > 2)
            {
                request.UserId = _user.Id;
            }
            else
            {
                user = _context.Users
                    .Include(x => x.Account)
                    .FirstOrDefault(x => x.Account.AccountNumber == request.AccountNumber);
                if (user == null)
                {
                    request.UserId = 0;
                }
                else
                {
                    request.UserId = user.Id;
                }
            }
            _validator.ValidateAndThrow(request);

            var calculation = _context.CreditCalculations
                .FirstOrDefault(x => x.CreditId == request.CreditId && x.MaxAmount >= request.Amount && x.MinAmount <= request.Amount && x.MinYear <= request.Years && x.MaxYear >= request.Years);
            var interest = calculation.Interest;

            var credit = new CreditUser
            {
                CreditId = request.CreditId,
                UserId = request.UserId,
                Years = request.Years,
                RemainingInstalments = request.Years * 12,
                Amount = request.Amount,
                MonthlyPayment = (request.Amount / (request.Years * 12)) * interest
            };

            var status = new CreditStatus
            {
                CreditUser = credit,
                CreatedAt = DateTime.Now,
                Active = true
            };

            if (user.RoleId > 2)
            {
                status.Status = Status.NaCekanju;
            }
            else
            {
                status.Status = Status.Odobren;
                user.Account.AvailableFunds += request.Amount;
                var bank = _context.Accounts
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.User.RoleId == 1);
                bank.AvailableFunds -= request.Amount;
            }

            _context.CreditUsers.Add(credit);
            _context.CreditStatuses.Add(status);
            _context.SaveChanges();
        }
    }
}
