using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class CreditUser
    {
        public int Id { get; set; }
        public int CreditId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int Years { get; set; }
        public decimal MonthlyPayment { get; set; }
        public int RemainingInstalments { get; set; }
        public Credit Credit { get; set; }
        public User User { get; set; }
        public IEnumerable<CreditStatus> CreditStatuses { get; set; } = new HashSet<CreditStatus>();
    }
}
