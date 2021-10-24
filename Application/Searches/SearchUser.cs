using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Searches
{
    public class SearchUser
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public int? PackageTypeId { get; set; }
        public decimal? MinAvaibleFounds { get; set; }
        public decimal? MaxAvaibleFounds { get; set; }

    }

}
