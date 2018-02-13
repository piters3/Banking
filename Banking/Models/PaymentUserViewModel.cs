using Banking.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class PaymentUserViewModel
    {
        public int Id { get; set; }

        public virtual BankAccount From { get; set; }

        [Display(Name = "Odbiorca/Nadawca")]
        public virtual BankAccount To { get; set; }

        [Display(Name = "Kwota operacji")]
        public double Amount { get; set; }

        [Display(Name = "Data")]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Opis operacji")]
        public string Title { get; set; }

        [Display(Name = "Rodzaj operacji")]
        public TypeOfOperation OperationType { get; set; }
    }
}