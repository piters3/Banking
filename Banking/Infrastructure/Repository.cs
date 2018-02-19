using Banking.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Banking.Infrastructure
{
    public class Repository : IRepository, IDisposable
    {
        private BankingContext _ctx;
        private bool disposed = false;

        public Repository(BankingContext ctx)
        {
            _ctx = ctx;
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public IEnumerable<User> GetUsers()
        {
            return _ctx.Users.ToList();
        }

        public User GetUser(string id)
        {
            return _ctx.Users.Find(id);
        }

        public void Insert(User user)
        {
            _ctx.Users.Add(user);
        }

        public void Update(User user)
        {
            _ctx.Entry(user).State = EntityState.Modified;
        }

        public void Delete(User user)
        {
            _ctx.Users.Remove(user);
        }

        public IEnumerable<BankAccount> GetBankAccounts()
        {
            return _ctx.BankAccounts.ToList();
        }

        public BankAccount GetBankAccount(Guid? accountNumber)
        {
            return _ctx.BankAccounts.Find(accountNumber);
        }

        public void Insert(BankAccount account)
        {
            _ctx.BankAccounts.Add(account);
        }

        public void Update(BankAccount account)
        {
            _ctx.Entry(account).State = EntityState.Modified;
        }

        public void Delete(BankAccount account)
        {
            _ctx.BankAccounts.Remove(account);
        }


        public IEnumerable<Payment> GetPayments()
        {
            return _ctx.Payments.ToList();
        }


        public Payment GetPayment(int id)
        {
            return _ctx.Payments.Find(id);
        }

        public async Task<Payment> GetPaymentAsync(int? id)
        {
            return await _ctx.Payments.FindAsync(id);
        }

        public void Insert(Payment payment)
        {
            _ctx.Payments.Add(payment);
        }

        public void Update(Payment payment)
        {
            _ctx.Entry(payment).State = EntityState.Modified;
        }

        public void Delete(Payment payment)
        {
            _ctx.Payments.Remove(payment);
        }


        public IEnumerable<Payment> GetUserPayments(string id)
        {
            return _ctx.Payments.Where(p => p.From.UserId == id).ToList().OrderByDescending(p => p.Id);
        }


        public BankAccount GetUserBankAccount(string id)
        {
            return _ctx.BankAccounts.Where(b => b.UserId == id).FirstOrDefault();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}