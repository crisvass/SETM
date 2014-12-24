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
        [Display(Name="Username")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Email")]        
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
        [Display(Name = "Uses Roles")]        
        public List<RoleView> UserRoles { get; set; }
        public SelectList UserRolesList { get; set; }

        //buyer
        [Display(Name = "Credit Cards")]
        public List<CreditCardDetailView> CreditCards { get; set; }

        //seller
        [Display(Name = "Delivery handled by Trader's Marketplace")]
        public bool RequiresDelivery { get; set; }
        [Display(Name = "IBAN Number")]
        public string IbanNumber { get; set; }
    }
}
