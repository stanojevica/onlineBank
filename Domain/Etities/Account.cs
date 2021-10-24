using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class Account : BaseEntity
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal AvailableFunds { get; set; }
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public User User { get; set; }
        public Package Package { get; set; }
    }
}
