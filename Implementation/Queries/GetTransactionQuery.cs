using Application.DataTransfer.Users.Transactions;
using Application.Interfaces;
using Application.Interfaces.Queries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class GetTransactionQuery : IGetTransactionQuery
    {
        public int Id => 20;

        public string Name => "Get transactions";

        private readonly Context _contex;
        private readonly IApplicationUser _user;
        private readonly IMapper _mapper;

        public GetTransactionQuery(IApplicationUser user, Context contex, IMapper mapper)
        {
            _user = user;
            _contex = contex;
            _mapper = mapper;
        }

        public IEnumerable<TransactionDto> Execute(SearchTransaction search)
        {
            var query = _contex.Transactions
                .Include(x => x.Recipient)
                .ThenInclude(x => x.Account)
                .ThenInclude(x => x.User)
                .Include(x => x.Sender)
                .ThenInclude(x => x.Account)
                .ThenInclude(x => x.User)
                .Where(x => x.SenderId == _user.Id || x.SenderId == _user.Id)
                .OrderByDescending(x => x.Date)
                .AsQueryable();

            if (search.Date.HasValue)
            {
                query = query.Where(x => x.Date == search.Date);
            }

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                var keyword = search.Keyword.ToLower();

                query = query.Where(x => 
                (x.SenderId == _user.Id && 
                (x.Recipient.Account.AccountNumber.ToLower().Contains(keyword) || x.Recipient.Account.User.Name.ToLower().Contains(keyword) || x.Recipient.Account.User.LastName.ToLower().Contains(keyword))) 
                ||
                (x.RecipientId == _user.Id &&
                (x.Sender.Account.AccountNumber.ToLower().Contains(keyword) ||x.Sender.Account.User.Name.ToLower().Contains(keyword) || x.Sender.Account.User.LastName.ToLower().Contains(keyword))));
            }

            var transactions = _mapper.Map<IEnumerable<TransactionDto>>(query);
            return transactions;
        }
    }
}
