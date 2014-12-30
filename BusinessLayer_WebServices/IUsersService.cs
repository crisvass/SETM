using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common.Views;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUsersService" in both code and config file together.
    [ServiceContract]
    public interface IUsersService
    {
        [OperationContract]
        void RegisterBuyer(string id, string name,
            string surname, string residence, string street, string town, string postCode,
            string country, string contactNumber, int creditCardTypeId, string creditCardNumber,
            string cardHolderName, int expiryDateMonth, int expiryDateYear);

        [OperationContract]
        void RegisterSeller(string id, string name,
            string surname, string residence, string street, string town, string postCode,
            string country, string contactNumber, bool requiresDelivery, string ibanNumber);

        //[OperationContract]
        //bool IsUserAuthenticated(string username, string password);

        [OperationContract]
        UserView GetUser(string id);

        [OperationContract]
        UserView GetUserForCheckout(string id);

        [OperationContract]
        IEnumerable<UserView> GetAllUsers();

        [OperationContract]
        void AddUser(string username, string password, string email, string name,
            string surname, string residence, string street, string town, string postCode,
            string country, string contactNumber, List<CreditCardDetailView> creditCards,
            bool requiresDelivery, string ibanNumber, List<RoleView> userRoles);

        [OperationContract(Name="UpdateUser")]
        void UpdateUser(string id, string email, string name,
            string surname, string residence, string street, string town, string postCode,
            string country, string contactNumber, List<CreditCardDetailView> creditCards,
            bool requiresDelivery, string ibanNumber, List<RoleView> userRoles);

        [OperationContract(Name="UpdateUserPartial")]
        void UpdateUser(string username, string email, string residence, string street, string town,
            string postCode, string country, string contactNumber, List<CreditCardDetailView> creditCards);

        [OperationContract]
        void DeleteUser(string id);
    }
}
