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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.RecipientId).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Purpose).IsRequired();
        }
    }
}
