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
    public class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.TypeId).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.Active).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(c => c.CreditUsers)
                .WithOne(u => u.Credit)
                .HasForeignKey(u => u.CreditId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CreditCalculations)
                .WithOne(cl => cl.Credit)
                .HasForeignKey(cl => cl.CreditId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CreditConditions)
                .WithOne(co => co.Credit)
                .HasForeignKey(co => co.CreditId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
