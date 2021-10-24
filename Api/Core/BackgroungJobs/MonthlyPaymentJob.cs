using Application.Email;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.BackgroungJobs
{
    public class MonthlyPaymentJob
    {
        private readonly Context _context;
        private readonly IEmailSender _email;

        public MonthlyPaymentJob(Context context, IEmailSender email)
        {
            _context = context;
            _email = email;
        }

        public void Charge()
        {
            var accounts = _context.CreditUsers
                .Include(x => x.User)
                .ThenInclude(x => x.Account);

            var adminAccount = _context.Accounts
                .Include(x => x.User)
                .FirstOrDefault(x => x.User.RoleId == 1);

            foreach (var i in accounts)
            { 
                if (i.User.Account.AvailableFunds < i.Amount)
                {
                    _email.Send(new EmailSenderDto
                    {
                        SendTo = i.User.Email,
                        Subject = "Mesečna naplata rate kredita",
                        Content = "Nemate dovoljno sredstava na racuču. Molimo vas da uplatite novac kako bismo mogli da vam naplatimo."
                    });
                }
                else
                {
                    i.User.Account.AvailableFunds -= i.MonthlyPayment;
                    adminAccount.AvailableFunds += i.MonthlyPayment;
                    _email.Send(new EmailSenderDto
                    {
                        SendTo = i.User.Email,
                        Subject = "Mesečna naplata rate kredita",
                        Content = "Poštovani klijente upravo vam je naplaćena rata kredita. Vaša ASP Banka."
                    });
                }
            }

            _context.SaveChanges();
        }
    }
}
