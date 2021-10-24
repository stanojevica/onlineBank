using Application.DataTransfer.Credits;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Queries
{
    public interface IGetCreditQuery : IQuery<SearchCredit, IEnumerable<ReadCreditDto>>
    {
    }
}
