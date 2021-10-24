using Domain.Etities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class CreditCalculationConfiguration : IEntityTypeConfiguration<CreditCalculation>
    {
        public void Configure(EntityTypeBuilder<CreditCalculation> builder)
        {
            builder.Property(x => x.Interest).IsRequired();
            builder.Property(x => x.MinAmount).IsRequired();
            builder.Property(x => x.MaxAmount).IsRequired();
            builder.Property(x => x.MinYear).IsRequired();
            builder.Property(x => x.MaxYear).IsRequired();
            builder.Property(x => x.CreditId).IsRequired();
        }
    }
}
