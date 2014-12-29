﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Views
{
    public class CartItemView
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Product")]
        public string ProductName { get; set; }
        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }
        public string ProductImagePath { get; set; }
        [Display(Name = "Quantity")]
        public int ProductQty { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
