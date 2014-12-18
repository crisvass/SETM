using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using Common.CustomExceptions;
using Common.Views;
using DataAccessLayer;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MenusService" in both code and config file together.
    public class MenusService : IMenusService
    {
        public IEnumerable<MenusView> GetMainMenus()
        {
            try
            {
                MenusRepository mr = new MenusRepository();
                RolesRepository rr = new RolesRepository();
                mr.Entity = rr.Entity;
                return mr.GetMainMenus(rr.GetGuestRole());
            }
            catch
            {
                throw new FaultException("Error occurred whilst loading menus. Please contact administrator if error persists.");
            }
        }

        public IEnumerable<MenusView> GetMainMenus(string username)
        {
            try
            {
                MenusRepository mr = new MenusRepository();
                UsersRepository ur = new UsersRepository();
                mr.Entity = ur.Entity;
                return mr.GetMainMenus(username);
                throw new NotImplementedException();
            }
            catch
            {
                throw new FaultException("Error occurred whilst loading menus. Please contact administrator if error persists.");
            }
        }


        public IEnumerable<MenusView> GetAllMainMenus()
        {
            return new MenusRepository().GetMainMenus();
        }

        public MenusView GetMenu(Guid id)
        {
            return new MenusRepository().GetMenuView(id);
        }

        public void AddMenu(string title, string action, string url, List<MenusView> submenus, List<RoleView> menuRoles)
        {
            try
            {
                MenusRepository mr = new MenusRepository();
                RolesRepository rr = new RolesRepository();
                mr.Entity = rr.Entity;
                Guid id = Guid.NewGuid();
                Menu m = new Menu()
                {
                    Id = id,
                    Title = title,
                    Action = action,
                    Url = url,
                    Position = mr.GetMainMenuLastPosition() + 1
                };

                try
                {
                    mr.Entity.Database.Connection.Open();
                    mr.Transaction = rr.Transaction = mr.Entity.Database.BeginTransaction();

                    mr.AddMenu(m);
                    foreach (MenusView sub in submenus)
                        mr.AddSubmenu(m, new Menu() 
                        {
 
                        });

                    foreach (RoleView role in menuRoles)
                        mr.AddMenuRole(m, rr.GetRole(role.RoleId));

                    mr.Transaction.Commit();
                }
                catch
                {
                    mr.Transaction.Rollback();
                    throw new TransactionFailedException("Adding a new Menu Failed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    mr.Entity.Database.Connection.Close();
                }
            }
            catch (TransactionFailedException ex)
            {
                throw ex;
            }
            catch
            {
                throw new FaultException("An error occurred whilst adding the new menu.");
            }
        }

        public void UpdateMenu(Guid id, string title, string action, string url, List<MenusView> submenus,
            List<RoleView> menuRoles)
        {
            try
            {
                MenusRepository mr = new MenusRepository();
                RolesRepository rr = new RolesRepository();
                mr.Entity = rr.Entity;
                Menu m = new Menu() { Id = id, Title = title, Action = action, Url = url };

                foreach (MenusView mv in mr.GetSubmenus(id))
                {
                    foreach (MenusView menu in submenus)
                    {
                        if (mv.MenuId == menu.MenuId)
                        {
                            m.Menus1.Add(new Menu()
                            {
                                Id = mv.MenuId,
                                Action = mv.Action,
                                Url = mv.Url,
                                Title = mv.Title,
                                Position = mv.Position,
                                ParentId = mv.ParentId
                            });
                        }
                    }
                }

                foreach (RoleView rv in mr.GetMenuRoles(id))
                {
                    foreach (RoleView role in menuRoles)
                    {
                        if (rv.RoleId == role.RoleId)
                            m.IdentityRoles.Add(rr.GetRole(rv.RoleId));
                    }
                }

                try
                {
                    mr.Entity.Database.Connection.Open();
                    mr.Transaction = rr.Transaction = mr.Entity.Database.BeginTransaction();

                    mr.UpdateMenu(m);

                    mr.Transaction.Commit();
                }
                catch
                {
                    mr.Transaction.Rollback();
                    throw new TransactionFailedException("Updating Menu Failed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    mr.Entity.Database.Connection.Close();
                }
            }
            catch (TransactionFailedException ex)
            {
                throw ex;
            }
            catch
            {
                throw new FaultException("An error occurred whilst updating the menu.");
            }
        }

        public void DeleteMenu(Guid id)
        {
            try
            {
                MenusRepository mr = new MenusRepository();
                try
                {
                    mr.Entity.Database.Connection.Open();
                    mr.Transaction = mr.Entity.Database.BeginTransaction();

                    mr.DeleteMenuRoles(id);
                    mr.DeleteSubmenus(id);
                    mr.DeleteMenu(id);

                    mr.Transaction.Commit();
                }
                catch
                {
                    mr.Transaction.Rollback();
                    throw new TransactionFailedException("Menu Deletion Failed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    mr.Entity.Database.Connection.Close();
                }
            }
            catch (TransactionFailedException ex)
            {
                throw ex;
            }
            catch
            {
                throw new FaultException("An error occurred whilst deleting the menu.");
            }
        }
    }
}
