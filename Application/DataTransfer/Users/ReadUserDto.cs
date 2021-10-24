using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Users
{
    public class ReadUserDto : UserDto
    {
        public string PackageType { get; set; }
        public string AccountNumber { get; set; }
        public decimal AvaibleFounds { get; set; }
        public DateTime CreateAt { get; set; }
        public List<string> MonthsRecidev { get; set; } = new List<string>();
        public List<string> MonthsSent { get; set; } = new List<string>();
        public List<TransactionPerMonths> Transactions { get; set; } = new List<TransactionPerMonths>();
        public IEnumerable<ReadTransactionsDto> SentTransactions { get; set; } = new HashSet<ReadTransactionsDto>();
        public IEnumerable<ReadTransactionsDto> ReciivedTransactions { get; set; } = new HashSet<ReadTransactionsDto>();
        public IEnumerable<ReadUserCredit> Credit { get; set; } = new HashSet<ReadUserCredit>();
    }
    public class TransactionPerMonths
    {
        public string Date { get; set; }
        public List<ReadTransactionsDto> ReciivedTransactions { get; set; } = new List<ReadTransactionsDto>();
        public List<ReadTransactionsDto> SentTransactions { get; set; } = new List<ReadTransactionsDto>();
    }
    public class ReadTransactionsDto
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Purpose { get; set; }
        public int RoleId { get; set; }
        public int Id { get; set; }
    }
    public class ReadUserCredit
    {
        public string CreditName { get; set; }
        public string CreditTypeName { get; set; }
        public string CreditStatus { get; set; }
        public decimal Amount { get; set; }
        public int Years { get; set; }
        public decimal MonthlyPayment { get; set; }
        public int RemainingInstalments { get; set; }
        public decimal Interest { get; set; }
    }
}
