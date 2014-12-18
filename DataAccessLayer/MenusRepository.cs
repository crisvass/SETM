using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Views;

namespace DataAccessLayer
{
    public class MenusRepository : ConnectionClass
    {
        public MenusRepository() : base() { }

        public IEnumerable<MenusView> GetMainMenus(IdentityRole r)
        {
            IEnumerable<MenusView> menus = r.Menus.Where(m => m.ParentId == null).Select(m => new MenusView()
            {
                MenuId = m.Id,
                Action = m.Action,
                Title = m.Title,
                Url = m.Url,
                Position = (int)m.Position
            }).OrderBy(m => m.Position).AsQueryable();

            List<MenusView> menusWithSubmenus = new List<MenusView>();
            foreach (MenusView mv in menus)
            {
                MenusView menu = mv;
                if (Entity.Menus.Where(m => m.ParentId == menu.MenuId).Count() > 0)
                {
                    menu.HasSubmenus = true;
                    menu.Submenus = Entity.Menus.Where(m => m.ParentId == menu.MenuId).Select(m => new MenusView()
                    {
                        MenuId = m.Id,
                        Action = m.Action,
                        Title = m.Title,
                        Url = m.Url,
                        Position = (int)m.Position
                    }).OrderBy(m => m.Position).AsQueryable();
                }
                menusWithSubmenus.Add(menu);
            }

            return menusWithSubmenus.AsQueryable();
        }

        public IEnumerable<MenusView> GetMainMenus(string username)
        {
            IEnumerable<MenusView> menus = (from ur in Entity.IdentityUserRoles
                                            join u in Entity.ApplicationUsers on ur.UserId equals u.Id
                                            join r in Entity.IdentityRoles on ur.RoleId equals r.Id
                                            from m in r.Menus
                                            where u.UserName == username && m.ParentId == null
                                            select new MenusView()
                                            {
                                                MenuId = m.Id,
                                                Action = m.Action,
                                                Title = m.Title,
                                                Url = m.Url,
                                                Position = (int)m.Position
                                            }).Distinct().OrderBy(m => m.Position).AsEnumerable();

            List<MenusView> menusWithSubmenus = new List<MenusView>();
            foreach (MenusView mv in menus)
            {
                MenusView menu = mv;
                if (Entity.Menus.Where(m => m.ParentId == menu.MenuId).Count() > 0)
                {
                    menu.HasSubmenus = true;
                    menu.Submenus = Entity.Menus.Where(m => m.ParentId == menu.MenuId).Select(m => new MenusView()
                    {
                        MenuId = m.Id,
                        Action = m.Action,
                        Title = m.Title,
                        Url = m.Url,
                        Position = (int)m.Position
                    }).OrderBy(m => m.Position).AsQueryable();
                }
                menusWithSubmenus.Add(menu);
            }

            return menusWithSubmenus.AsQueryable();
        }

        public IEnumerable<MenusView> GetMainMenus()
        {
            IEnumerable<MenusView> menus = Entity.Menus.Where(m => m.ParentId == null)
                .Select(m => new MenusView()
                {
                    MenuId = m.Id,
                    Action = m.Action,
                    Title = m.Title,
                    Url = m.Url,
                    Position = (int)m.Position
                });

            List<MenusView> menusWithSubmenus = new List<MenusView>();
            foreach (MenusView mv in menus)
            {
                MenusView menu = mv;
                if (Entity.Menus.Where(m => m.ParentId == menu.MenuId).Count() > 0)
                {
                    menu.HasSubmenus = true;
                    menu.Submenus = Entity.Menus.Where(m => m.ParentId == menu.MenuId).Select(m => new MenusView()
                    {
                        MenuId = m.Id,
                        Action = m.Action,
                        Title = m.Title,
                        Url = m.Url,
                        Position = (int)m.Position
                    }).OrderBy(m => m.Position).AsQueryable(); ;
                }
                menusWithSubmenus.Add(menu);
            }

            return menusWithSubmenus.OrderBy(m => m.Position).AsQueryable();
        }

        private Menu GetMenu(Guid id)
        {
            return Entity.Menus.SingleOrDefault(m => m.Id == id);
        }

        public MenusView GetMenuView(Guid id)
        {
            Menu menu = GetMenu(id);
            MenusView mv = new MenusView()
                        {
                            MenuId = menu.Id,
                            Action = menu.Action,
                            Title = menu.Title,
                            Url = menu.Url,
                            Position = (int)menu.Position                            
                        };
            
            if (Entity.Menus.Where(m => m.ParentId == mv.MenuId).Count() > 0)
            {
                mv.HasSubmenus = true;
                mv.Submenus = Entity.Menus.Where(m => m.ParentId == mv.MenuId).Select(m => new MenusView()
                {
                    MenuId = m.Id,
                    Action = m.Action,
                    Title = m.Title,
                    Url = m.Url,
                    Position = (int)m.Position
                }).OrderBy(m => m.Position).AsQueryable();
            }

            mv.MenuRoles = menu.IdentityRoles.Select(r => new RoleView() { RoleId = r.Id, RoleName = r.Name });

            return mv;
        }

        public void AddMenu(Menu m)
        {
            Entity.Menus.Add(m);
            Entity.SaveChanges();
        }

        public void AddSubmenu(Menu pm, Menu sm)
        {
            pm.Menus1.Add(sm);
            Entity.SaveChanges();
        }

        public void AddMenuRole(Menu m, IdentityRole r)
        {
            m.IdentityRoles.Add(r);
            Entity.SaveChanges();
        }

        public void UpdateMenu(Menu updatedMenu)
        {
            Menu menu = GetMenu(updatedMenu.Id);
            menu.Action = updatedMenu.Action;
            menu.Url = updatedMenu.Url;
            menu.Title = updatedMenu.Title;
            menu.Menus1 = updatedMenu.Menus1;
            menu.IdentityRoles = updatedMenu.IdentityRoles;
            Entity.SaveChanges();
        }

        public void DeleteMenu(Guid id)
        {
            Entity.Menus.Remove(GetMenu(id));
            Entity.SaveChanges();
        }

        public void DeleteSubmenus(Guid id)
        {
            GetMenu(id).Menus1.Clear();
            Entity.SaveChanges();
        }

        //public void DeleteSubmenu(Guid pId, Guid sId)
        //{
        //    GetMenu(pId).Menus1.Remove(GetMenu(sId));
        //    Entity.SaveChanges();
        //}

        public void DeleteMenuRoles(Guid id)
        {
            GetMenu(id).IdentityRoles.Clear();
            Entity.SaveChanges();
        }

        public void DeleteMenuRole(Guid mId, IdentityRole r)
        {
            GetMenu(mId).IdentityRoles.Remove(r);
            Entity.SaveChanges();
        }

        public IEnumerable<MenusView> GetSubmenus(Guid id)
        {
            return GetMenu(id).Menus1.Select(m => new MenusView() { MenuId = m.Id });
        }

        public IEnumerable<RoleView> GetMenuRoles(Guid id)
        {
            return GetMenu(id).IdentityRoles.Select(r => new RoleView() { RoleId = r.Id });
        }

        public int GetMainMenuLastPosition()
        {
            return GetMainMenus().OrderByDescending(m => m.Position).FirstOrDefault().Position;
        }
    }
}
