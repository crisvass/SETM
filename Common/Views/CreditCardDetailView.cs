using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Views
{
    public class CreditCardDetailView
    {
        public int Id { get; set; }

        public int CreditCardTypeId { get; set; }

        [Required]
        [Display(Name = "Credit Card")]
        public string CreditCardType { get; set; }
        public SelectList CreditCardTypeList { get; set; }

        [Required]
        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }

        [Required]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public int Month { get; set; }
        public SelectList MonthsList { get; set; }
        [Required]
        public int Year { get; set; }
        public SelectList YearsList { get; set; }
    }
}
