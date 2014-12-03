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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CreditCardsService" in both code and config file together.
    public class CreditCardsService : ICreditCardsService
    {
        public IEnumerable<CreditCardTypeView> GetCreditCardTypes()
        {
            try
            {
                return new CreditCardsRepository().GetCardTypes();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
        }
    }
}
