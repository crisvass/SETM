using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class CheckoutView
    {
        public UserView Uv { get; set; }
        public CreditCardDetailView Ccdv { get; set; }
        [Required]
        [Display(Name="CVV for Card Selected")]
        public string Cvv { get; set; }
    }
}
