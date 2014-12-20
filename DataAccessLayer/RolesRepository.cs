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
            return (from ur in Entity.IdentityUserRoles
                    join u in Entity.ApplicationUsers on ur.UserId equals u.Id
                    join r in Entity.IdentityRoles on ur.RoleId equals r.Id
                    where u.UserName == username
                    select new RoleView()
                    {
                        RoleId = r.Id,
                        RoleName = r.Name
                    });
        }

        public void AllocateRole(ApplicationUser u, IdentityRole r)
        {
            u.IdentityUserRoles.Add(new IdentityUserRole() { ApplicationUser = u, IdentityRole = r });
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

        public IEnumerable<RoleView> GetRoles()
        {
            return Entity.IdentityRoles.Select(r => new RoleView() { RoleId = r.Id, RoleName = r.Name });
        }

        public void AddRole(IdentityRole r)
        {
            Entity.IdentityRoles.Add(r);
            Entity.SaveChanges();
        }

        public void UpdateRole(IdentityRole updatedRole)
        {
            IdentityRole role = Entity.IdentityRoles.SingleOrDefault(r => r.Id == updatedRole.Id);
            role.Name = updatedRole.Name;
            Entity.SaveChanges();
        }

        public IdentityRole GetRole(string id)
        {
            return Entity.IdentityRoles.SingleOrDefault(r => r.Id == id);
        }

        public RoleView GetRoleView(string id)
        {
            IdentityRole r = GetRole(id);
            return new RoleView() { RoleId = r.Id, RoleName = r.Name };
        }

        public void DeleteRole(string id)
        {
            Entity.IdentityRoles.Remove(GetRole(id));
            Entity.SaveChanges();
        }

        public void DeleteMenuRoles(string id)
        {
            GetRole(id).Menus.Clear();
            Entity.SaveChanges();
        }

        public bool RoleIsAssigned(string id)
        {
            return Entity.IdentityUserRoles.Count(ur => ur.RoleId == id) > 0;
        }

        public IEnumerable<RoleView> GetNonMenuAssignedRoles(Guid id)
        {
            return (from r in Entity.IdentityRoles
                    where !(from m in Entity.Menus
                            from role in m.IdentityRoles
                            where m.Id == id
                            select role.Id)
                            .Contains(r.Id)
                    select new RoleView()
                    {
                        RoleId = r.Id,
                        RoleName = r.Name
                    });
        }
    }
}
