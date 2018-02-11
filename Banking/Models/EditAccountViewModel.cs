using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class EditAccountViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d{2}-\d{3}?$", ErrorMessage = "Nieprawidłowy kod pocztowy")]
        public string PostalCode { get; set; }
    }
}