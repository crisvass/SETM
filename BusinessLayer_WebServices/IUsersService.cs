using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUsersService" in both code and config file together.
    [ServiceContract]
    public interface IUsersService
    {
        [OperationContract]
        void RegisterBuyer(string id, string name,
            string surname, string residence, string street, string town, string postCode,
            string country, int creditCardTypeId, string cardHolderName, int expiryDateMonth,
            int expiryDateYear);

        [OperationContract]
        void RegisterSeller(string id, string name,
            string surname, string residence, string street, string town, string postCode,
            string country, bool requiresDelivery, string ibanNumber);

        [OperationContract]
        bool IsUserAuthenticated(string username, string password);
    }
}
