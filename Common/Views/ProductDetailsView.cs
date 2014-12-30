using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class ProductDetailsView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public int QtyAvailable { get; set; }
        public string SellerUsername { get; set; }
    }
}
