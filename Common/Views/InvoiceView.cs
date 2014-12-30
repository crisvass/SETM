using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class InvoiceView
    {
        public Guid OrderId { get; set; }
        public UserView Uv { get; set; }
        public List<OrderDetailView> Items { get; set; }
        [Display(Name = "Sub-Total")]
        public decimal Subtotal { get; set; }
        [Display(Name = "VAT %")]
        public int VatRate { get; set; }
        public decimal VatAmount { get; set; }
        [Display(Name = "Total")]
        public decimal Total { get; set; }
    }
}
