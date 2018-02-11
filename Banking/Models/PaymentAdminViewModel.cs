using Banking.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class PaymentAdminViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nadawca")]
        public Guid From { get; set; }

        [Display(Name = "Odbiorca")]
        public Guid To { get; set; }

        [Display(Name = "Kwota operacji")]
        public double Amount { get; set; }

        [Display(Name = "Data operacji")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Rodzaj operacji")]
        public TypeOfOperation OperationType { get; set; }

        public virtual BankAccount Account { get; set; }
    }
}