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

        public IEnumerable<MenusView> GetMainMenus(Role r)
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
            IEnumerable<MenusView> menus = (from u in Entity.Users
                                           from r in u.Roles
                                           from m in r.Menus
                                           where u.Username == username && m.ParentId == null
                                           select new MenusView()
                                           {
                                               MenuId = m.Id,
                                               Action = m.Action,
                                               Title = m.Title,
                                               Url = m.Url,
                                               Position = (int)m.Position
                                           }
                   ).Distinct().OrderBy(m => m.Position).AsQueryable();

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
    }
}
