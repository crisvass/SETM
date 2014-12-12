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
            return (from u in Entity.ApplicationUsers
                    from r in u.IdentityRoles
                    where u.UserName == username
                    select new RoleView()
                    {
                        RoleId = (int)r.Id,
                        Role = r.Name
                    });
        }

        public void AllocateRole(ApplicationUser u, IdentityRole r)
        {
            u.IdentityRoles.Add(r);
            Entity.SaveChanges();
        }

        public IdentityRole GetBuyerRole()
        {
            return Entity.IdentityRoles.SingleOrDefault(r => r.Name.Trim().ToLower() == "buyer");
        }

        public IdentityRole GetSellerRole()
        {
            IdentityRole role = Entity.IdentityRoles.SingleOrDefault(r => r.Name.Trim().ToLower() == "seller");
            return role;
        }

        public IdentityRole GetGuestRole()
        {
            return Entity.IdentityRoles.SingleOrDefault(r => r.Name.Trim().ToLower() == "guest");
        }
    }
}
