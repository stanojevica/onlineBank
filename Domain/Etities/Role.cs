using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Etities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Role Parent { get; set; }
        public virtual ICollection<Role> Children { get; set; } = new HashSet<Role>();
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
