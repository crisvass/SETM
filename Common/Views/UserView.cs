using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Views
{
    public class UserView
    {
        public string Id { get; set; }
        
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
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Email Address")]   
        public string Email { get; set; }

        [Display(Name = "First Name")]       
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]   
        public string LastName { get; set; }
        
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        
        [Display(Name = "Residence")]
        public string Residence { get; set; }
        
        [Display(Name = "Street")]        
        public string Street { get; set; }
        
        [Display(Name = "Post Code")]        
        public string PostCode { get; set; }
        
        [Display(Name = "Town")]        
        public string Town { get; set; }
        
        [Display(Name = "Country")]
        public string Country { get; set; }
        
        [Required]
        [Display(Name = "Uses Roles")]        
        public List<RoleView> UserRoles { get; set; }
        public SelectList UserRolesList { get; set; }

        //buyer
        [Display(Name = "Credit Cards")]
        public List<CreditCardDetailView> CreditCards { get; set; }

        //seller
        [Display(Name = "Delivery handled by Trader's Marketplace")]
        public bool RequiresDelivery { get; set; }
        
        [RegularExpression("[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}", ErrorMessage = "IBAN not valid.")]
        [Display(Name = "IBAN Number")]
        public string IbanNumber { get; set; }
    }
}
