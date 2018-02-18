using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class NewPaymentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa odbiorcy")]
        public string RecipentName { get; set; }

        [Required]
        [Display(Name = "Numer rachunku")]
        public Guid AccountNumber { get; set; }

        [Display(Name = "Adres odbiorcy")]
        public string RecipentAddress { get; set; }

        [Required]
        [Display(Name = "Kwota")]
        public double Amount { get; set; }

        [Required]
        [Display(Name = "Data przelewu")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Tytuł przelewu")]
        public string Title { get; set; }
    }
}