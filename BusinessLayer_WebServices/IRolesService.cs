using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using Common.Views;

namespace BusinessLayer_WebServices
{
    [ServiceContract]
    public interface IRolesService
    {
        [OperationContract]
        IEnumerable<RoleView> GetRoles();

        [OperationContract]
        void AddRole(string name);

        [OperationContract]
        void UpdateRole(string id, string name);

        [OperationContract]
        RoleView GetRole(string id);

        [OperationContract]
        void DeleteRole(string id);
    }
}
