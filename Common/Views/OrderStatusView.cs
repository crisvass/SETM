using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class OrderStatusView
    {
        [Display(Name="Order Status ID")]
        public int OrderStatusId { get; set; }
        [Display(Name="Order Status Name")]
        public string OrderStatus { get; set; }
    }
}
