using Banking.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banking.Infrastructure
{
    public interface IRepository
    {
        void Save();
        Task SaveAsync();

        IEnumerable<User> GetUsers();
        User GetUser(string id);
        void Insert(User user);
        void Delete(User user);
        void Update(User user);

        IEnumerable<BankAccount> GetBankAccounts();
        BankAccount GetBankAccount(Guid? accountNumber);
        void Insert(BankAccount account);
        void Delete(BankAccount account);
        void Update(BankAccount account);

        IEnumerable<Payment> GetPayments();
        Payment GetPayment(int id);
        Task<Payment> GetPaymentAsync(int? id);
        void Insert(Payment payment);
        void Delete(Payment payment);
        void Update(Payment payment);

        IEnumerable<Payment> GetUserPayments(string id);
        BankAccount GetUserBankAccount(string id);
    }
}