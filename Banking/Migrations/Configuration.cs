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
            var adminRole = new IdentityRole { Name = "admin", Id = Guid.NewGuid().ToString() };
            var userRole = new IdentityRole { Name = "user", Id = Guid.NewGuid().ToString() };
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);

            var hasher = new PasswordHasher();

            var adminBankAccount = new BankAccount() { };
            var userBankAccount = new BankAccount() { };
            var user2BankAccount = new BankAccount() { };

            List<Payment> adminPayments = new List<Payment>()
            {
                new Payment(){
                    From = adminBankAccount,
                    To = userBankAccount,
                    Amount = 999,
                    PaymentDate = DateTime.Now,
                    Title = "Czynsz za luty",
                    OperationType = TypeOfOperation.TransferToAccount
                },
                 new Payment(){
                    From = adminBankAccount,
                    To = userBankAccount,
                    Amount = 12345,
                    PaymentDate = DateTime.Now,
                    Title = "Skoki narciarskie",
                    OperationType = TypeOfOperation.CardPayment
                }
            };


            List<Payment> userPayments = new List<Payment>()
            {
                new Payment(){
                    From = userBankAccount,
                    To = adminBankAccount,
                    Amount = 50,
                    PaymentDate = DateTime.Now,
                    Title = "A masz",
                    OperationType = TypeOfOperation.TransferToAccount
                }
            };

            List<Payment> user2Payments = new List<Payment>()
            {
                new Payment(){
                    From = user2BankAccount,
                    To = adminBankAccount,
                    Amount = 768,
                    PaymentDate = DateTime.Now,
                    Title = "Ram",
                    OperationType = TypeOfOperation.TransferToAccount
                },
                  new Payment(){
                    From = user2BankAccount,
                    To = userBankAccount,
                    Amount = 5799,
                    PaymentDate = DateTime.Now,
                    Title = "Komputer",
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
                Name = "Piotr",
                Surname = "Strzelecki",
                Address = "ul. Rymwida 4/18",
                City = "Lublin",
                PostalCode = "20-607",
                PhoneNumber = "517906254",
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
                Name = "Mateusz",
                Surname = "Szpinda",
                Address = "ul. Zamoyskiego 42/26",
                City = "Zamoœæ",
                PostalCode = "22-400",
                PhoneNumber = "798634092",
                Accounts = new List<BankAccount> { userBankAccount }
            };


            user.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });

            context.Users.Add(user);


            var user2 = new User
            {
                UserName = "user2",
                Email = "use2r@user.com",
                PasswordHash = hasher.HashPassword("user2"),
                EmailConfirmed = true,
                Enabled = true,
                RegisterDate = DateTime.Today,
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = "Piotr",
                Surname = "Sobiborowicz",
                Address = "ul. Batalionów Ch³opskich 75",
                City = "Rossosz",
                PostalCode = "21-533",
                PhoneNumber = "713465789",
                Accounts = new List<BankAccount> { user2BankAccount }
            };


            user2.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = user.Id });

            context.Users.Add(user2);

            adminPayments.ForEach(m => context.Payments.AddOrUpdate(x => x.Id, m));
            context.SaveChanges();

            userPayments.ForEach(m => context.Payments.AddOrUpdate(x => x.Id, m));
            context.SaveChanges();

            user2Payments.ForEach(m => context.Payments.AddOrUpdate(x => x.Id, m));
            context.SaveChanges();
        }
    }
}
