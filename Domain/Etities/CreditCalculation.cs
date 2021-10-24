using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class CreditCalculation
    {
        public int Id { get; set; }
        public int CreditId { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
        public decimal Interest { get; set; }
        public Credit Credit { get; set; }
    }
}
