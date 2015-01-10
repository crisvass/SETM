using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessLayer_WebServices.DataContracts;
using Common;
using Common.Views;

namespace BusinessLayer_WebServices
{
    [ServiceContract]
    public interface IRolesService
    {
        [OperationContract]
        IEnumerable<RoleView> GetUserRoles(string username);

        [OperationContract]
        IEnumerable<RoleView> GetRoles();

        [OperationContract]
        IdentityRole AddRole(string name);

        [OperationContract]
        IdentityRole UpdateRole(string id, string name);

        [OperationContract]
        [FaultContract(typeof(ConstraintFail))]
        RoleView GetRole(string id);

        [OperationContract]
        void DeleteRole(string id);

        [OperationContract]
        IEnumerable<RoleView> GetNonMenuAssignedRoles(Guid id);

        [OperationContract]
        IEnumerable<RoleView> GetNonUserAssignedRoles(String id);
    }
}
