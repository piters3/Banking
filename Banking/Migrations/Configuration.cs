namespace Banking.Migrations
{
    using Banking.Entities;
    using Banking.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BankingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BankingContext context)
        {
            //if (!System.Diagnostics.Debugger.IsAttached)
            //    System.Diagnostics.Debugger.Launch();
            AddUsersAndRoles(context);
        }

        private static void AddUsersAndRoles(BankingContext context)
        {
            var adminRole = new IdentityRole { Name = "admin", Id = Guid.NewGuid().ToString() };
            var userRole = new IdentityRole { Name = "user", Id = Guid.NewGuid().ToString() };
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);

            var hasher = new PasswordHasher();



            var adminBankAccount = new BankAccount() { };
            var userBankAccount = new BankAccount() { };

            List<Payment> adminPayments = new List<Payment>()
            {
                new Payment(){
                    From = adminBankAccount.AccountNumber,
                    To = userBankAccount.AccountNumber,
                    Amount = 999,
                    PaymentDate = DateTime.Now,
                    Title = "Czynsz za luty",
                    OperationType = TypeOfOperation.TransferToAccount
                },
                 new Payment(){
                    From = adminBankAccount.AccountNumber,
                    To = userBankAccount.AccountNumber,
                    Amount = 12345,
                    PaymentDate = DateTime.Now,
                    Title = "Skoki narciarskie",
                    OperationType = TypeOfOperation.CardPayment
                }
            };



            List<Payment> userPayments = new List<Payment>()
            {
                new Payment(){
                    From = userBankAccount.AccountNumber,
                    To = adminBankAccount.AccountNumber,
                    Amount = 50,
                    PaymentDate = DateTime.Now,
                    Title = "A masz",
                    OperationType = TypeOfOperation.TransferToAccount
                }
            };

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@admin.com",
                PasswordHash = hasher.HashPassword("admin"),
                EmailConfirmed = true,
                Enabled = true,
                RegisterDate = DateTime.Today,
                SecurityStamp = Guid.NewGuid().ToString(),
                Accounts = new List<BankAccount> { adminBankAccount }
            };

            admin.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = admin.Id });

            context.Users.Add(admin);

            var user = new User
            {
                UserName = "user",
                Email = "user@user.com",
                PasswordHash = hasher.HashPassword("user"),
                EmailConfirmed = true,
                Enabled = true,
                RegisterDate = DateTime.Today,
                SecurityStamp = Guid.NewGuid().ToString(),
                Accounts = new List<BankAccount> { userBankAccount }
            };

            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });

            context.Users.Add(user);

            adminPayments.ForEach(m => context.Payments.AddOrUpdate(x => x.Id, m));
            context.SaveChanges();

            userPayments.ForEach(m => context.Payments.AddOrUpdate(x => x.Id, m));
            context.SaveChanges();
        }
    }
}
