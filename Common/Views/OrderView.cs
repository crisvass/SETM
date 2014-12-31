using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Views
{
    public class OrderView
    {
        [Display(Name = "Order ID")]
        public Guid OrderId { get; set; }
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate;
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Vat Rate")]
        public int VatRate { get; set; }
        public int StatusId { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        public SelectList StatusList { get; set; }
        [Display(Name = "Order Total")]
        public decimal OrderTotal { get; set; }
        [Display(Name = "Order Items")]
        public IEnumerable<OrderDetailView> OrderItems { get; set; }
    }
}
