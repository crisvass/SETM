using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Common.Views;

namespace TradersMarketplace.Models
{
    public class UserCrud
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Residence { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public List<RoleView> UserRoles { get; set; }
    }

    public class UserDbcontext : DbContext
    {
        public DbSet<UserCrud> Users { get; set; }

        public System.Data.Entity.DbSet<Common.Views.ProductDetailsView> ProductDetailsViews { get; set; }

        public System.Data.Entity.DbSet<Common.Views.CategoryView> CategoryViews { get; set; }

        public System.Data.Entity.DbSet<Common.Views.ProductListView> ProductListViews { get; set; }

        public System.Data.Entity.DbSet<Common.Views.CartItemView> CartItemViews { get; set; }
    }
}