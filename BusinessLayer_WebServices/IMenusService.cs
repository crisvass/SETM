using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
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
    }
}
