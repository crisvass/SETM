using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Views;

namespace TradersMarketplace.Models
{
    public class ProductSearchViewModel
    {
        [Display(Name = "Product Name")]
        public string NameKeyword { get; set; }
        [Display(Name = "Product Description")]
        public string DescKeyword { get; set; }
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        public SelectList CategoriesList { get; set; }
        [Display(Name = "Subcategory")]
        public Guid SubcategoryId { get; set; }
        public SelectList SubcategoriesList { get; set; }

        public ProductSearchViewModel()
        {
            ProductCategoriesServiceClient.ProductCategoriesServiceClient pcs =
                new ProductCategoriesServiceClient.ProductCategoriesServiceClient();

            CategoriesList = new SelectList(pcs.GetMainCategories(), "CategoryId", "CategoryName");
            SubcategoriesList = new SelectList(new List<CategoryView>(), "CategoryId", "CategoryName");
        }
    }
}