using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TradersMarketplace.Models
{
    public class RegisterViewModel
    {
        public List<object> RegisterModels;
    }

    public class RegisterBuyerViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Residence")]
        public string Residence { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Town")]
        public string Town { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Credit Card")]
        public int CreditCardType { get; set; }
        public SelectList CreditCardTypesList { get; set; }

        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }

        [Display(Name = "Expiry Date")]
        public int Month { get; set; }
        public SelectList MonthsList { get; set; }
        public int Year { get; set; }
        public SelectList YearsList { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be betwen 6 and 20 characters long.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Passoword must include atleast one lowercase letter, one uppercase letter, one digit, one special character and be atleast 8 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public class RegisterSellerViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Residence")]
        public string Residence { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Town")]
        public string Town { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "IBAN Number")]
        public string IbanNumber { get; set; }

        [Display(Name = "Let Trader's Marketplace handle delivery of the products you sell")]
        public bool RequiresDelivery { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be betwen 6 and 20 characters long.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Passoword must include atleast one lowercase letter, one uppercase letter, one digit, one special character and be atleast 8 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be betwen 6 and 20 characters long.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Passoword must include atleast one lowercase letter, one uppercase letter, one digit, one special character and be atleast 8 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

}
