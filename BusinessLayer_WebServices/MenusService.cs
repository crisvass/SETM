using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
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
                //return mr.GetMainMenus(username);
                throw new NotImplementedException();
            }
            catch
            {
                throw new FaultException("Error occurred whilst loading menus. Please contact administrator if error persists.");
            }
        }
    }
}
