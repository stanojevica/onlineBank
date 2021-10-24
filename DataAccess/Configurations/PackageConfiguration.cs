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
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.AccountMaintenance).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(p => p.Accounts)
                .WithOne(a => a.Package)
                .HasForeignKey(a => a.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
