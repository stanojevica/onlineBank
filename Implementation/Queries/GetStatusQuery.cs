using Application.DataTransfer.Credits.Status;
using Application.Interfaces.Queries;
using Application.Searches;
using DataAccess;
using Domain.Etities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public class GetStatusQuery : IGetStatusQuery
    {
        public int Id => 30;

        public string Name => "Get statuses";

        public IEnumerable<StatusDto> Execute(SearchStatus search)
        {

            List<StatusDto> statusi = new List<StatusDto>();
            var countEnum = Enum.GetNames(typeof(Status)).Length;
            for (var i = 0; i < countEnum; i++)
            {
                statusi.Add(new StatusDto
                {
                    Id = i,
                    StatusName = (Status)i
                });
            }

            foreach(var i in statusi)
            {
                i.Name = i.StatusName.ToString();
            }
            return statusi;
        }
    }
}
