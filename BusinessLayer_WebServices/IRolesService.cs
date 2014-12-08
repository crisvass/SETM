using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common.Views;

namespace BusinessLayer_WebServices
{
    [ServiceContract]
    public interface IRolesService
    {
        [OperationContract]
        IQueryable<RoleView> GetUserRoles(string username);
    }
}
