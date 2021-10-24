using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class Credit : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public CreditType CreditType { get; set; }
        public virtual ICollection<CreditCondition> CreditConditions { get; set; } = new HashSet<CreditCondition>();
        public virtual ICollection<CreditCalculation> CreditCalculations { get; set; } = new HashSet<CreditCalculation>();
        public virtual ICollection<CreditUser> CreditUsers { get; set; } = new HashSet<CreditUser>();
    }
}
