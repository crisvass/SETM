using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class CreditCardTypeView
    
    {
        [Display(Name="Credit Card Type ID")]
        public int CreditCardTypeId { get; set; }
        [Display(Name = "Credit Card Type Name")]
        public string CreditCardType { get; set; }
    }
}
