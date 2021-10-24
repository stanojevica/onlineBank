using Application.DataTransfer.Credits;
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
    public class GetCreditQuery : IGetCreditQuery
    {
        public int Id => 14;

        public string Name => "Get info about redit";

        private readonly Context _context;
        private readonly IMapper _mapper;

        public GetCreditQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ReadCreditDto> Execute(SearchCredit search)
        {
            var query = _context.Credits
                .Include(x => x.CreditConditions)
                .Include(x => x.CreditCalculations)
                .Include(x => x.CreditType)
                .Where(x => x.Active == true && x.DeleteAt == null)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.TypeName))
            {
                query = query.Where(x => x.CreditType.Name.ToLower().Contains(search.Name.ToLower()));
            }

            var credits = _mapper.Map<IEnumerable<ReadCreditDto>>(query);

            if (credits.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(Credit));
            }

            return credits;
        }
    }
}
