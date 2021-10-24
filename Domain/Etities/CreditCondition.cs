using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class CreditCondition
    {
        public int Id { get; set; }
        public int CreditId { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public Credit Credit { get; set; }
    }
}
