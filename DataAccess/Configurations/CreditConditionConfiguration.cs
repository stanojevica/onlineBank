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
    public class CreditConditionConfiguration : IEntityTypeConfiguration<CreditCondition>
    {
        public void Configure(EntityTypeBuilder<CreditCondition> builder)
        {
            builder.Property(x => x.CreditId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Condition).IsRequired();
        }
    }
}
