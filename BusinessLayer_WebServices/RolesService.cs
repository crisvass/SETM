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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RolesService" in both code and config file together.
    public class RolesService : IRolesService
    {
        public IQueryable<RoleView> GetUserRoles(string username)
        {
            try
            {
                //return new RolesRepository().GetUserRoles(username);
                throw new NotImplementedException();
            }
            catch
            {
                throw new FaultException("Error whilst retrieving user roles. Please contact administrator if error persists.");
            }
        }
    }
}
