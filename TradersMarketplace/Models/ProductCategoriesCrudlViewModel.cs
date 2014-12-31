using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TradersMarketplace.Models
{
    public class ProductCategoriesCrudl
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductCategoriesCrudl> SubCategories { get; set; }
    }

    public class Dbcontext : DbContext
    {
        public DbSet<ProductCategoriesCrudl> OrderStatuses { get; set; }
    }
}