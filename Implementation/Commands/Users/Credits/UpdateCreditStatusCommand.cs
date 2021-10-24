using Application.DataTransfer.Users.Credits;
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
    public class UpdateCreditStatusCommand : IUpadteCreditStatusCommand
    {
        public int Id => 17;

        public string Name => "Update credit status";

        private readonly Context _context;
        private readonly UpdateCreditStatusValidator _validator;

        public UpdateCreditStatusCommand(Context context, UpdateCreditStatusValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public void Execute(CreditStatusDto request)
        {
            _validator.ValidateAndThrow(request);

            var creditUser = _context.CreditUsers
                .FirstOrDefault(x => x.UserId == request.UserId);

            var lastStatus = _context.CreditStatuses
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault(x => x.CreditUserId == creditUser.Id);
            if(lastStatus.Status == 0 && (int)request.Status == 1)
            {
                var user = _context.Users
                    .Include(x => x.Account)
                    .FirstOrDefault(x => x.Id == request.UserId);
                user.Account.AvailableFunds += creditUser.Amount;
                var bank = _context.Accounts
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.User.RoleId == 1);
                bank.AvailableFunds -= creditUser.Amount;
            }
            var creditStatus = new CreditStatus
            {
                CreditUserId = creditUser.Id,
                Status = (Status)request.Status,
                Active = true,
                CreatedAt = DateTime.Now
            };

            _context.CreditStatuses.Add(creditStatus);
            _context.SaveChanges();
        }
    }
}
