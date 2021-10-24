using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Account Account { get; set; }
        public virtual ICollection<Transaction> TransactionSenders { get; set; } = new HashSet<Transaction>();
        public virtual ICollection<Transaction> TransactionRecipients { get; set; } = new HashSet<Transaction>();
        public virtual ICollection<CreditUser> CreditUsers { get; set; } = new HashSet<CreditUser>();
    }
}
