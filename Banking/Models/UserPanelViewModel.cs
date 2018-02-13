using System.Collections.Generic;

namespace Banking.Models
{
    public class UserPanelViewModel
    {
        public BankAccountViewModel BankAccount { get; set; }
        public IEnumerable<PaymentUserViewModel> Payments { get; set; }
    }
}