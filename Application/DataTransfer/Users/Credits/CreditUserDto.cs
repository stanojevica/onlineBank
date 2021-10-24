using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Users.Credits
{
    public class CreditUserDto
    {
        public int CreditId { get; set; }
        public string AccountNumber { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int Years { get; set; }
    }
}
