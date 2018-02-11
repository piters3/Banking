using Banking.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Banking.Infrastructure
{
    public class BankingContext : IdentityDbContext<User>
    {
        public BankingContext()
            : base("Banking", throwIfV1Schema: false)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //                .HasOptional(u => u.Account)
        //                .WithRequired(a => a.User);
        //    base.OnModelCreating(modelBuilder);

        //}

        public static BankingContext Create()
        {
            return new BankingContext();
        }
    }
}