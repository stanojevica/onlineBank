using Application.DataTransfer.Credits.Status;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries
{
    public interface IGetStatusQuery : IQuery<SearchStatus, IEnumerable<StatusDto>>
    {
    }
}
