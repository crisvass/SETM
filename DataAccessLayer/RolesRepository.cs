using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccessLayer
{
    public class RolesRepository : ConnectionClass
    {
        public RolesRepository() : base() { }

        public void AllocateRole(User u, Role r)
        {
            u.Roles.Add(r);
            Entity.SaveChanges();
        }

        public Role GetBuyerRole()
        {
            return Entity.Roles.SingleOrDefault(r => r.Role1.Trim().ToLower() == "buyer");
        }

        public Role GetSellerRole()
        {
            return Entity.Roles.SingleOrDefault(r => r.Role1.Trim().ToLower() == "seller");
        }
    }
}
