using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class EditBankAccountViewModel
    {
        [Display(Name = "Numer konta")]
        public Guid AccountNumber { get; set; }

        [Display(Name = "Blokady")]
        public double Locks { get; set; }

        [Display(Name = "Zamrożenie konta")]
        public bool Frozen { get; set; }

        [Display(Name = "Saldo")]
        public double Balance { get; set; }

        [Display(Name = "Limit debetowy")]
        public double DebitLimit { get; set; }
    }
}