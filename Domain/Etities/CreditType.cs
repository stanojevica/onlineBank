using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class CreditType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Credit> Credits { get; set; } = new HashSet<Credit>();
    }
}
