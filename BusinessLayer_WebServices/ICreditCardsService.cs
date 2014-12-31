using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common.Views;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICreditCardsService" in both code and config file together.
    [ServiceContract]
    public interface ICreditCardsService
    {
        [OperationContract]
        IEnumerable<CreditCardTypeView> GetCreditCardTypes();

        [OperationContract]
        CreditCardTypeView GetCreditCardType(int id);

        [OperationContract]
        void AddCreditCardType(string name);

        [OperationContract]
        void UpdateCreditCardType(int id, string name);

        [OperationContract]
        void DeleteCreditCardType(int id);
    }
}
