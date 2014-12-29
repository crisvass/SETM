using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using Common.CustomExceptions;
using DataAccessLayer;
using Common.Views;

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
            try
            {
                return new MenusRepository().GetMainMenus();
            }
            catch
            {
                throw new FaultException("An error occurred whilst retrieving the menus.");
            }
        }

        public MenusView GetMenu(Guid id)
        {
            try
            {
                return new MenusRepository().GetMenuView(id);
            }
            catch
            {
                throw new FaultException("An error occurred whilst retrieving the menu details.");
            }
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
                            Id = Guid.NewGuid(),
                            Title = sub.Title,
                            Action = sub.Action,
                            Url = sub.Url,
                            Position = sub.Position
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
                throw new FaultException(ex.Message);
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

                try
                {
                    mr.Entity.Database.Connection.Open();
                    mr.Transaction = rr.Transaction = mr.Entity.Database.BeginTransaction();
                    List<MenusView> submenusOriginal = mr.GetSubmenus(id).ToList<MenusView>();
                    List<RoleView> menuRolesOriginal = mr.GetMenuRoles(id).ToList<RoleView>();
                    mr.UpdateMenu(m);

                    bool found;

                    //delete removed submenus
                    foreach (MenusView mv in submenusOriginal)
                    {
                        found = false;
                        foreach (MenusView menu in submenus)
                        {
                            if (mv.MenuId == menu.MenuId)
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            mr.DeleteMenu(mv.MenuId);
                        }
                    }

                    //add new submenus
                    foreach (MenusView mv in submenus)
                    {
                        found = false;
                        foreach (MenusView menu in submenusOriginal)
                        {
                            if (mv.MenuId == menu.MenuId)
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            mr.AddSubmenu(mr.GetMenu(id), new Menu() {
                                Id = Guid.NewGuid(),
                                Title = mv.Title,
                                Action = mv.Action,
                                Url = mv.Url,
                                Position = mv.Position
                            });
                        }
                    }
                    
                    //delete removed menu roles
                    foreach (RoleView rv in menuRolesOriginal)
                    {
                        found = false;
                        foreach (RoleView role in menuRoles)
                        {
                            if (rv.RoleId == role.RoleId)
                                found = true;                            
                        }

                        if (!found)
                            mr.DeleteMenuRole(id, rr.GetRole(rv.RoleId));
                    }

                    //add new menu roles
                    foreach (RoleView rv in menuRoles)
                    {
                        found = false;
                        foreach (RoleView role in menuRolesOriginal)
                        {
                            if (rv.RoleId == role.RoleId)
                                found = true;
                        }

                        if (!found)
                            mr.AddMenuRole(mr.GetMenu(id), rr.GetRole(rv.RoleId));
                    }
                    
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
                throw new FaultException(ex.Message);
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

                    List<MenusView> submenus = mr.GetSubmenus(id).ToList();
                    foreach(MenusView mv in submenus)
                    {
                        mr.DeleteMenu(mv.MenuId);
                    } 
                    mr.DeleteMenuRoles(id);
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
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("An error occurred whilst deleting the menu.");
            }
        }
    }
}
