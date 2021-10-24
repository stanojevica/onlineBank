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
    public class CreditStatusConfiguration : IEntityTypeConfiguration<CreditStatus>
    {
        public void Configure(EntityTypeBuilder<CreditStatus> builder)
        {
            builder.Property(x => x.CreditUserId).IsRequired();
            builder.Property(x => x.Status).IsRequired();

        }
    }
}
