using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data.Entity;
using System.Data.Common;

namespace DataAccessLayer
{
    public class ConnectionClass
    {
        public dbTradersMarketplaceEntities Entity { get; set; }
        public DbTransaction Transaction { get; set; }

        public ConnectionClass()
        {
            Entity = new dbTradersMarketplaceEntities();
        }
    }
}
