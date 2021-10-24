using Application.Email;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.BackgroungJobs
{
    public class MonthlySubmissionReportsJob
    {
        private readonly Context _context;
        private readonly IEmailSender _sender;
        public MonthlySubmissionReportsJob(Context context, IEmailSender sender)
        {
            _context = context;
            _sender = sender;
        }

        public void Send()
        {
            var users = _context.Users
                .Include(x => x.Account)
                .Include(x => x.TransactionRecipients)
                .ThenInclude(x => x.Sender)
                .Include(x => x.TransactionSenders)
                .ThenInclude(x => x.Recipient)
                .Where(x => x.RoleId > 3);

            var lastMonthFullDate = DateTime.Now.AddMonths(-1);
            var lastMonth = lastMonthFullDate.Month;
            var maxDayInMont = DateTime.DaysInMonth(lastMonthFullDate.Year, lastMonth);

            var firstInMounthString = lastMonth + "/1/" + lastMonthFullDate.Year;
            var firstInMounth = Convert.ToDateTime(firstInMounthString);
            var lastInMounthString = lastMonth + "/" +  maxDayInMont + "/" + lastMonthFullDate.Year;
            var lastInMounth = Convert.ToDateTime(lastInMounthString);

            var contect = new List<ContentDto>();
            string poslateTranstakcije = "";
            string primljeneTransakcije = "";

            foreach(var i in users)
            {
                foreach (var j in i.TransactionRecipients)
                {
                    if(j.Recipient.Id == i.Id)
                    {
                        primljeneTransakcije += "Datum :" + j.Date + " |Posiljalac:" + j.Sender.Name + " " + j.Sender.LastName + " |Opis: " + j.Purpose + " |Iznos: " + j.Amount + "<br/>";
                    }
                }
                if (primljeneTransakcije == "")
                {
                    primljeneTransakcije = "Nije bilo transakcija u proteklom mesecu.";
                }

                foreach (var j in i.TransactionSenders)
                {
                    if(j.Sender.Id == i.Id && j.Date >= firstInMounth && j.Date <= lastInMounth)
                    {
                        poslateTranstakcije += "Datum :" + j.Date + " |Primalac:" + j.Recipient.Name + " " + j.Recipient.LastName + " |Opis: " + j.Purpose + " |Iznos: " + j.Amount + "<br/>";
                    }
                }
                if (poslateTranstakcije == "")
                {
                    poslateTranstakcije = "Nije bilo transakcija u proteklom mesecu.";
                }

                decimal amount = i.Account.AvailableFunds + (i.TransactionSenders.Where(x => x.Date >= firstInMounth && x.Date <= lastInMounth).Sum(x => x.Amount) - i.TransactionRecipients.Where(x => x.Date >= firstInMounth && x.Date <= lastInMounth).Sum(x => x.Amount));
                contect.Add(new ContentDto
                {
                    UserId = i.Id,
                    Content = "<h4>" + i.Name + " " + i.LastName + "</h4> <br/>Period od 1." + lastMonth + "." + lastMonthFullDate.Year + " do " + maxDayInMont + "." + lastMonth + "." + lastMonthFullDate.Year + "<br/>Broj računa: " + i.Account.AccountNumber + "<br/>Valuta: RSD <br/><br/>Stanje na početku meseca: " + amount + "<br/>Ukupni priliv novca u toku tekućeg meseca: " + i.TransactionRecipients.Where(x => x.Date >= firstInMounth && x.Date <= lastInMounth).Sum(x => x.Amount) + "<br/>Ukupni odliv novca u toku tekućeg meseca: " + i.TransactionSenders.Where(x => x.Date >= firstInMounth && x.Date <= lastInMounth).Sum(x => x.Amount) + "<br/>Trenutno stanje računa: " + i.Account.AvailableFunds + "<br/> <br/>Pregled svih poslatih transakcija: <br/>" + poslateTranstakcije + "<br/>Pregled svih primljenih transakcija: <br/>"
                    + primljeneTransakcije

                });

                primljeneTransakcije = "";
                poslateTranstakcije = "";
            }

            foreach (var i in users)
            {
                foreach(var j in contect)
                {
                    if(i.Id == j.UserId)
                    {
                        _sender.Send(new EmailSenderDto
                        {
                            Subject = "Mesečni izveštaj klijenata",
                            SendTo = i.Email,
                            Content = j.Content
                        });
                    }

                }
                
            }
            
        }
    }

    public class ContentDto
    {
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
