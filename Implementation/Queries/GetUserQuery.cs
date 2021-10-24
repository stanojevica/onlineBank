using Application.DataTransfer.Users;
using Application.Exceptions;
using Application.Interfaces.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domain.Etities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class GetUserQuery : IGetUserQuery
    {
        public int Id => 19;

        public string Name => "Dohvatanje informacija o korisniku";

        private readonly Context _context;
        private readonly IMapper _mapper;

        public GetUserQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ReadUserDto> Execute(SearchUser search)
        {
            var query = _context.Users
                .Include(x => x.Account)
                .ThenInclude(x => x.Package)
                .Include(x => x.TransactionRecipients)
                .ThenInclude(x => x.Sender)
                .ThenInclude(x => x.Account)
                .Include(x => x.TransactionSenders)
                .ThenInclude(x => x.Recipient)
                .ThenInclude(x => x.Account)
                .Include(x => x.CreditUsers)
                .ThenInclude(x => x.Credit)
                .ThenInclude(x => x.CreditType)
                .Include(x => x.CreditUsers)
                .ThenInclude(x => x.CreditStatuses)
                .Include(x => x.CreditUsers)
                .ThenInclude(x => x.Credit)
                .ThenInclude(x => x.CreditCalculations)
                .Where(x => x.RoleId > 3 && x.Active == true && x.DeleteAt == null)
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                var s = search.Name.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(s)
                                    || x.LastName.ToLower().Contains(s)
                                    || x.Email.ToLower().Contains(s));
            }
            if (!string.IsNullOrEmpty(search.AccountNumber))
            {
                query = query.Where(x => x.Account.AccountNumber == search.AccountNumber);
            }
            if (search.Id.HasValue)
            {
                query = query.Where(x => x.Id == search.Id);
            }
            if (search.MaxAvaibleFounds.HasValue)
            {
                query = query.Where(x => x.Account.AvailableFunds <= search.MaxAvaibleFounds);
            }
            if (search.MinAvaibleFounds.HasValue)
            {
                query = query.Where(x => x.Account.AvailableFunds >= search.MinAvaibleFounds);
            }
            if (search.PackageTypeId.HasValue)
            {
                query = query.Where(x => x.Account.PackageId == search.PackageTypeId);
            }
            var users = _mapper.Map<IEnumerable<ReadUserDto>>(query);

            if (users.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            var rangeMonths = new List<string>();

            foreach(var i in users)
            {
                foreach(var d in i.MonthsSent)
                {
                    if (rangeMonths.IndexOf(d) == -1)
                    {
                        rangeMonths.Add(d);
                    }
                }
                foreach (var d in i.MonthsRecidev)
                {
                    if (rangeMonths.IndexOf(d) == -1)
                    {
                        rangeMonths.Add(d);
                    }
                }
                
                i.MonthsRecidev = rangeMonths;
            }

            foreach(var m in rangeMonths)
            {
                
                foreach(var u in users)
                {
                    if(u.ReciivedTransactions.Count() == 0 && u.SentTransactions.Count() == 0)
                    {
                        break;
                    }
                    var transaction = new TransactionPerMonths();
                    transaction.Date = m;
                    foreach (var t in u.ReciivedTransactions)
                    {
                       var monthRecivedString = t.Date.Month.ToString() + "-" + t.Date.Year.ToString();
                        if (monthRecivedString == m && u.Id == t.Id)
                        {
                            transaction.ReciivedTransactions.Add(t);
                        }
                    }
                    foreach (var t in u.SentTransactions)
                    {
                        var monthRecivedString = t.Date.Month.ToString() + "-" + t.Date.Year.ToString();
                        if (monthRecivedString == m && u.Id == t.Id)
                        {
                            transaction.SentTransactions.Add(t);
                        }
                    }
                    u.Transactions.Add(transaction);
                }
            }
            
            return users;
            
        }
    }
}
