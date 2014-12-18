using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TradersMarketplace.MenusServiceClient;

namespace TradersMarketplace.Models
{
    public class MenusCrud
    {
        [Key]
        public Guid MenuId { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public bool HasSubmenus { get; set; }
        public IEnumerable<MenusCrud> Submenus { get; set; }
    }
    
    public class MenuCrudDbContext : DbContext
    {
        public DbSet<MenusCrud> Menus { get; set; }
    }
}