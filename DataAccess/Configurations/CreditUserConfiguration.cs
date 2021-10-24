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
    public class CreditUserConfiguration : IEntityTypeConfiguration<CreditUser>
    {
        public void Configure(EntityTypeBuilder<CreditUser> builder)
        {
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.CreditId).IsRequired();
            builder.Property(x => x.MonthlyPayment).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Years).IsRequired();
            builder.Property(x => x.RemainingInstalments).IsRequired();

            builder.HasMany(c => c.CreditStatuses)
                .WithOne(s => s.CreditUser)
                .HasForeignKey(s => s.CreditUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
