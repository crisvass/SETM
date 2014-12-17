using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TradersMarketplace.Models
{
    public class RoleCrud
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class RoleDBContext : DbContext
    {
        public DbSet<RoleCrud> Roles { get; set; }
    }
}