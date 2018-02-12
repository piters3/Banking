using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Banking.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            Accounts = new List<BankAccount>();
        }

        public virtual List<BankAccount> Accounts { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        [Display(Name = "Login")]
        public override string UserName { get; set; }

        [Display(Name = "Aktywacja mailem")]
        public override bool EmailConfirmed { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }

        [Display(Name = "Aktywność")]
        public bool Enabled { get; set; }

        [Display(Name = "Data rejestracji")]
        public DateTime RegisterDate { get; set; }

        public override string ToString()
        {
            if (Name != null)
            {
                return Name + " " + Surname + ", " + Address + ", " + PostalCode + " " + City;
            }
            else
                return null;
        }
    }
}