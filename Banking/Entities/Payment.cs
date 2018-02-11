using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Entities
{
    public enum TypeOfOperation
    {
        [Display(Name = "Przelew na rachunek")]
        TransferToAccount,
        [Display(Name = "Płatność kartą")]
        CardPayment,
        [Display(Name = "Wpłata gotówki we wpłatomacie")]
        DepositoryCashPayment
    }


    public class Payment
    {
        public int Id { get; set; }
        public Guid From { get; set; }
        public Guid To { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Title { get; set; }
        public TypeOfOperation OperationType { get; set; }

        public virtual BankAccount Account { get; set; }
    }
}