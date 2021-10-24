using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Credits
{
    public class ReadCreditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NameOfType { get; set; }
        public int IdType { get; set; }
        public virtual ICollection<CreditConditionDto> Conditions { get; set; } = new HashSet<CreditConditionDto>();
        public virtual ICollection<CreditCalculationDto> Calculations { get; set; } = new HashSet<CreditCalculationDto>();

    }

}
