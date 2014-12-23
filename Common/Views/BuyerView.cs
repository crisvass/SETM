using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class BuyerView
    {
        [Display(Name = "Credit Cards")]
        public List<CreditCardDetailView> CreditCards { get; set; }
    }
}
