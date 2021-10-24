using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Credits
{
    public class CreditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditType { get; set; }
        public IEnumerable<CreditCalculationDto> CreditCalculations { get; set; } = new HashSet<CreditCalculationDto>();
        public IEnumerable<CreditConditionDto> CreditConditions { get; set; } = new HashSet<CreditConditionDto>();
    }

    public class CreditCalculationDto 
    {
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public int MinAmout { get; set; }
        public int MaxAmount { get; set; }
        public decimal Interest { get; set; }

    }
    public class CreditComparer : IEqualityComparer<CreditCalculationDto>
    {
        public bool Equals(CreditCalculationDto x, CreditCalculationDto y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.MinYear == y.MinYear && x.MaxYear == y.MaxYear && x.Interest == y.Interest && x.MinAmout == y.MinAmout && x.MaxAmount == y.MaxAmount;
        }
        public int GetHashCode([DisallowNull] CreditCalculationDto obj) => obj.MinYear + obj.MaxYear + obj.MinAmout + obj.MaxAmount;
    }

    public class CreditConditionDto
    {
        public string Name { get; set; }
        public string Condition { get; set; }
    }
}
