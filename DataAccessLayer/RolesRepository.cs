using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AllocateRole(ApplicationUser u, string roleId)
        {
            Entity.IdentityUserRoles.Add(new IdentityUserRole() { UserId = u.Id, RoleId = roleId });
            Entity.SaveChanges();
        }

        public void DeleteUserRole(string userId, string roleId)
        {
            Entity.IdentityUserRoles.Remove(
                Entity.IdentityUserRoles.SingleOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId));
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
            return Entity.IdentityRoles
                .Select(r => new RoleView() { RoleId = r.Id, RoleName = r.Name })
                .Where(r => r.RoleName.ToLower() != "guest");
        }

        public IdentityRole AddRole(IdentityRole r)
        {
            IdentityRole role = Entity.IdentityRoles.Add(r);
            Entity.SaveChanges();
            return role;
        }

        public IdentityRole UpdateRole(IdentityRole updatedRole)
        {
            IdentityRole role = Entity.IdentityRoles.SingleOrDefault(r => r.Id == updatedRole.Id);
            role.Name = updatedRole.Name;
            Entity.SaveChanges();
            return new IdentityRole() { Id = role.Id, Name = role.Name };
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

        public IEnumerable<RoleView> GetNonUserAssignedRoles(String id)
        {
            return (from r in Entity.IdentityRoles
                    where !(from ur in Entity.IdentityUserRoles
                            join role in Entity.IdentityRoles on ur.RoleId equals role.Id
                            join u in Entity.ApplicationUsers on ur.UserId equals u.Id
                            where u.Id == id
                            select role.Id)
                            .Contains(r.Id) && r.Name.ToLower() != "guest"
                    select new RoleView()
                    {
                        RoleId = r.Id,
                        RoleName = r.Name
                    });
        }
    }
}
