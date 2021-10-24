using Application.DataTransfer.Users.Transactions;
using Application.Interfaces;
using Application.Interfaces.Commands.UsersCommands.Transactions;
using DataAccess;
using Domain.Etities;
using FluentValidation;
using Implementation.Validators.Users.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands.Users.Transactions
{
    public class TransactionCommand : ITransactionCommand
    {
        public int Id => 18;

        public string Name => "Transakcije";

        private readonly Context _context;
        private readonly TransactionValidator _validator;
        private readonly IApplicationUser _user;
        public TransactionCommand(Context context, TransactionValidator validator, IApplicationUser user)
        {
            _context = context;
            _validator = validator;
            _user = user;
        }

        public void Execute(TransactionDto request)
        {
            _validator.ValidateAndThrow(request);
            var user = _context.Users.Find(_user.Id);
            var sender = _context.Accounts.FirstOrDefault(x => x.UserId == _user.Id); ;
            if(user.RoleId == 2)
            {
                sender = _context.Accounts.FirstOrDefault(x => x.User.RoleId == 1);
                sender.AvailableFunds += 50;
            }       

            var recipient = _context.Accounts.FirstOrDefault(x => x.AccountNumber == request.RecipientAccountNumber);
            var transaction = new Transaction
            {
                SenderId = sender.UserId,
                RecipientId = recipient.UserId,
                Amount = request.Amount,
                Purpose = request.Purpose,
                Date = DateTime.Now
            };

            recipient.AvailableFunds += request.Amount;

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            
        }
    }
}
