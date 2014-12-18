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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMenusService" in both code and config file together.
    [ServiceContract]
    public interface IMenusService
    {
        [OperationContract(Name="GetGuestMenus")]
        IEnumerable<MenusView> GetMainMenus();

        [OperationContract(Name = "GetUserMenus")]
        IEnumerable<MenusView> GetMainMenus(string username);

        [OperationContract]
        IEnumerable<MenusView> GetAllMainMenus();

        [OperationContract]
        MenusView GetMenu(Guid id);

        [OperationContract]
        void AddMenu(string title, string action, string url, List<MenusView> submenus, List<RoleView> menuRoles);

        [OperationContract]
        void UpdateMenu(Guid id, string title, string action, string url, List<MenusView> submenus, List<RoleView> menuRoles);

        [OperationContract]
        void DeleteMenu(Guid id);
    }
}
