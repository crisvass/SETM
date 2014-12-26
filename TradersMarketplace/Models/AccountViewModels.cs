using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ServiceModel;
using System.Web.Mvc;

namespace TradersMarketplace.Models
{
    //public class RegisterViewModel
    //{
    //    public List<object> RegisterModels;
    //}

    public class RegisterBuyerViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be betwen 6 and 20 characters long.")]
        [Display(Name = "Username*")]
        public string BuyerUsername { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Passoword must include atleast one lowercase letter, one uppercase letter, one digit, one special character and be atleast 8 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        public string BuyerPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password*")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("BuyerPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string BuyerConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Email Address*")]
        public string BuyerEmail { get; set; }

        //[Required]
        [Display(Name = "First name")]
        public string BuyerName { get; set; }

        //[Required]
        [Display(Name = "Last Name")]
        public string BuyerSurname { get; set; }

        //[Required]
        [Display(Name = "Residence")]
        public string BuyerResidence { get; set; }

        //[Required]
        [Display(Name = "Street")]
        public string BuyerStreet { get; set; }

        //[Required]
        [Display(Name = "Town")]
        public string BuyerTown { get; set; }

        //[Required]
        [Display(Name = "Post Code")]
        public string BuyerPostCode { get; set; }

        //[Required]
        [Display(Name = "Country")]
        public string BuyerCountry { get; set; }

        [Display(Name = "Contact Number")]
        public string BuyerContactNumber { get; set; }

        [Display(Name = "Credit Card")]
        public int BuyerCreditCardType { get; set; }
        public SelectList BuyerCreditCardTypesList { get; set; }

        [Display(Name = "Credit Card Number")]
        public string BuyerCreditCardNumber { get; set; }

        [Display(Name = "Card Holder Name")]
        public string BuyerCardHolderName { get; set; }

        [Display(Name = "Expiry Date")]
        public int BuyerMonth { get; set; }
        public SelectList BuyerMonthsList { get; set; }
        public int BuyerYear { get; set; }
        public SelectList BuyerYearsList { get; set; }

        public RegisterBuyerViewModel()
        {
            try
            {
                BuyerCreditCardTypesList = new SelectList(new CreditCardsServiceClient.CreditCardsServiceClient().GetCreditCardTypes(), 
                    "CreditCardTypeId", "CreditCardType");
            }
            catch (FaultException ex)
            {
                throw ex;
            }

            List<MonthModel> months = new List<MonthModel>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(new MonthModel() { MonthNum = i, Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i) });
            }
            string[] years = new string[5];

            for (int i = 0; i < years.Length; i++)
            {
                years[i] = (DateTime.Now.Year + i).ToString();
            }

            BuyerMonthsList = new SelectList(months, "MonthNum", "Month");
            BuyerYearsList = new SelectList(years);
        }
    }

    public class RegisterSellerViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Username must be betwen 6 and 20 characters long.")]
        [Display(Name = "Username*")]
        public string SellerUsername { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Passoword must include atleast one lowercase letter, one uppercase letter, one digit, one special character and be atleast 8 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        public string SellerPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password*")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("SellerPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string SellerConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Email Address*")]
        public string SellerEmail { get; set; }

        [Required]
        [Display(Name = "First name*")]
        public string SellerName { get; set; }

        [Required]
        [Display(Name = "Last Name*")]
        public string SellerSurname { get; set; }

        [Required]
        [Display(Name = "Residence*")]
        public string SellerResidence { get; set; }

        [Required]
        [Display(Name = "Street*")]
        public string SellerStreet { get; set; }

        [Required]
        [Display(Name = "Town*")]
        public string SellerTown { get; set; }

        [Required]
        [Display(Name = "Post Code*")]
        public string SellerPostCode { get; set; }

        [Required]
        [Display(Name = "Country*")]
        public string SellerCountry { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string SellerContactNumber { get; set; }

        [Required]
        [Display(Name = "IBAN Number*")]
        public string SellerIbanNumber { get; set; }

        [Display(Name = "Let Trader's Marketplace handle delivery of the products you sell")]
        public bool SellerRequiresDelivery { get; set; }
    }

    public class MonthModel
    {
        public int MonthNum { get; set; }
        public string Month { get; set; }
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
