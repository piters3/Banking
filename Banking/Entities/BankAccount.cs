using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Banking.Entities
{
    public class BankAccount
    {
        public BankAccount()
        {
            AccountNumber = Guid.NewGuid();
            Locks = 0;
            Balance = 0;
            DebitLimit = 0;
            AvailableFunds = 0;
            OpeningDate = DateTime.Today;
            Frozen = false;
            Payments = new List<Payment>();
        }

        [Key]
        public Guid AccountNumber { get; set; }
        public DateTime OpeningDate { get; set; }
        public double Locks { get; set; }
        public double Balance { get; set; }
        public double DebitLimit { get; set; }

        private double _availableFunds;

        public double AvailableFunds
        {
            get
            {
                return _availableFunds;
            }
            set
            {
                _availableFunds = DebitLimit + Balance - Locks;
            }
        }

        public bool Frozen { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Payment> Payments { get; set; }
    }
}