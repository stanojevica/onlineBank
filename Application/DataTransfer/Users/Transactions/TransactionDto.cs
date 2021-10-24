using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Users.Transactions
{
    public class TransactionDto
    {
        public string RecipientAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Purpose { get; set; }
    }
}
