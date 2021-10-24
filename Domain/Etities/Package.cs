using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class Package : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AccountMaintenance { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}
