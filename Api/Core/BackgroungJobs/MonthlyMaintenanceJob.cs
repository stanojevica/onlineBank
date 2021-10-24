using Application.Email;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.BackgroungJobs
{
    public class MonthlyMaintenanceJob
    {
        private readonly Context _context;
        private readonly IEmailSender _email;

        public MonthlyMaintenanceJob(Context context, IEmailSender email)
        {
            _context = context;
            _email = email;
        }

        public void Charge()
        {
            var accounts = _context.Accounts
                .Include(x => x.Package)
                .Include(x => x.User);

            var adminAccount = _context.Accounts
                .Include(x => x.User)
                .FirstOrDefault(x => x.User.RoleId == 1);
            foreach(var i in accounts)
            {
                if(i.AvailableFunds < i.Package.AccountMaintenance)
                {
                    _email.Send(new EmailSenderDto
                    {
                        SendTo = i.User.Email,
                        Subject = "Mesečno održavanje računa",
                        Content = "Nemate dovoljno sredstava na racuču. Molimo vas da uplatite novac kako bismo mogli da vam naplatimo."
                    });
                }
                else
                {
                    i.AvailableFunds -= i.Package.AccountMaintenance;
                    adminAccount.AvailableFunds += i.Package.AccountMaintenance;
                    _email.Send(new EmailSenderDto
                    {
                        SendTo = i.User.Email,
                        Subject = "Mesečna naplata rate kredita",
                        Content = "Poštovani klijente upravo vam je naplaćeno mesčno održavanje računa. Vaša ASP Banka."
                    });
                }
                
            }
            _context.SaveChanges();
        }
    }
}
