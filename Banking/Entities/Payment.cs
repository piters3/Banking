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
        PaymentToATM,
        [Display(Name = "Wypłata z bankomatu")]
        WithdrawalFromATM
    }


    public class Payment
    {
        public int Id { get; set; }
        public virtual BankAccount From { get; set; }
        public virtual BankAccount To { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Title { get; set; }
        public TypeOfOperation OperationType { get; set; }

        //private double _balanceAfterOperation;

        public double BalanceAfterOperation { get; set; }

        //public double BalanceAfterOperation
        //{
        //    get
        //    {
        //        return _balanceAfterOperation;
        //    }
        //    private set
        //    {
        //        _balanceAfterOperation = To.Balance;
        //    }
        //}
    }
}