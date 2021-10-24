using Application.DataTransfer.Packages;
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
    public class GetPackageQuery : IGetPackageQuery
    {
        public int Id => 15;

        public string Name => "Get info aubout bank packages";

        private readonly Context _context;
        private readonly IMapper _mapper;

        public GetPackageQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ReadPackageDto> Execute(SearchPackage search)
        {
            var query = _context.Packages
                .Include(x => x.Accounts)
                .Where(x => x.Active == true && x.DeleteAt == null)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()) || x.Description.ToLower().Contains(search.Name.ToLower()));
            }

            if (search.MinMaintenance.HasValue)
            {
                query.Where(x => x.AccountMaintenance > search.MinMaintenance);
            }

            if (search.MaxMaintenance.HasValue)
            {
                query.Where(x => x.AccountMaintenance < search.MaxMaintenance);
            }

            var pacages = _mapper.Map<IEnumerable<ReadPackageDto>>(query);

            if (pacages.Count() == 0)
            {
                throw new EntityNotFoundException(typeof(Package));
            }

            return pacages;
        }
    }
}
