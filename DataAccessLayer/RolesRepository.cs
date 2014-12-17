﻿using System;
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

        //public IQueryable<RoleView> GetUserRoles(string username)
        //{
        //return (from u in entity.applicationusers
        //        from r in u.identityroles
        //        where u.username == username
        //        select new roleview()
        //        {
        //            roleid = guid.parse(r.id),
        //            role = r.name
        //        });
        //}

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

        public RoleView GetRole(string id)
        {
            return Entity.IdentityRoles.Select(r => new RoleView() { RoleId = r.Id, RoleName = r.Name }).SingleOrDefault(r => r.RoleId == id);
        }

        public void DeleteRole(string id)
        {
            Entity.IdentityRoles.Remove(Entity.IdentityRoles.SingleOrDefault(r => r.Id == id));
            Entity.SaveChanges();
        }

        public void DeleteMenuRoles(string id)
        {
            Entity.IdentityRoles.SingleOrDefault(r => r.Id == id).Menus.Clear();
            Entity.SaveChanges();
        }

        public bool RoleIsAssigned(string id)
        {
            return Entity.IdentityUserRoles.Count(ur => ur.RoleId == id) > 0;
        }
    }
}
