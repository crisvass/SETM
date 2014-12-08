using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Views;

namespace DataAccessLayer
{
    public class RolesRepository : ConnectionClass
    {
        public RolesRepository() : base() { }

        public IQueryable<RoleView> GetUserRoles(string username)
        {
            return (from u in Entity.Users
                    from r in u.Roles
                    where u.Username == username
                    select new RoleView()
                    {
                        RoleId = r.Id,
                        Role = r.Role1
                    });
        }

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

        public Role GetGuestRole()
        {
            return Entity.Roles.SingleOrDefault(r => r.Role1.Trim().ToLower() == "guest");
        }
    }
}
