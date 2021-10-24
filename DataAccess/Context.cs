using DataAccess.Configurations;
using Domain.Etities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-KJVU7VEO;Initial Catalog=AspBank;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCalculationConfiguration());
            modelBuilder.ApplyConfiguration(new CreditConditionConfiguration());
            modelBuilder.ApplyConfiguration(new CreditConfiguration());
            modelBuilder.ApplyConfiguration(new CreditTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PackageConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new CreditStatusConfiguration());

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<CreditCalculation> CreditCalculations { get; set; }
        public DbSet<CreditCondition> CreditConditions { get; set; }
        public DbSet<CreditType> CreditTypes { get; set; }
        public DbSet<CreditUser> CreditUsers { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CreditStatus> CreditStatuses { get; set; }

    }
}
