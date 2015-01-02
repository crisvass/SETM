using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common.Views
{
    public class ProductView
    {
        public int ProductId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
        public SelectList CategoriesList { get; set; }
        [Display(Name = "Subcategory")]
        public Guid SubcategoryId { get; set; }
        [Display(Name = "Subcategory")]
        public string Subcategory { get; set; }
        public SelectList SubcategoriesList { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
        [Display(Name = "Quantity in Stock")]
        public int QtyAvailable { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        //public int SellerId { get; set; }
        public int CommissionTypeId { get; set; }
        [Display(Name = "Commission Type")]
        public string CommissionType { get; set; }
        public SelectList CommissionTypesList { get; set; }
        [Display(Name = "Commission Amount")]
        public decimal CommissionAmount { get; set; }
    }
}
