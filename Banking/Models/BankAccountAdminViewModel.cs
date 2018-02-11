using Banking.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class BankAccountAdminViewModel
    {
        [Display(Name = "Numer konta")]
        public Guid AccountNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data otwarcia")]
        public DateTime OpeningDate { get; set; }

        [Display(Name = "Blokady")]
        public double Locks { get; set; }

        [Display(Name = "Zamrożenie konta")]
        public bool Frozen { get; set; }

        [Display(Name = "Saldo")]
        public double Balance { get; set; }

        [Display(Name = "Limit debetowy")]
        public double DebitLimit { get; set; }

        [Display(Name = "Dostępne środki")]
        public double AvailableFunds { get; set; }

        //[Required]
        [Display(Name = "Użytkownik")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}