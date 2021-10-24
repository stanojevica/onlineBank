using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class CreditStatus
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public int CreditUserId { get; set; }
        public CreditUser CreditUser { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum Status
    {
        NaCekanju,
        Odobren,
        NijeOdobren,
        Pauziran,
        Aktivan,
        Isplacen
    }
}
