using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using Common.CustomExceptions;
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
            catch
            {
                throw new FaultException("An error occurred whilst retrieving the credit card types.");
            }
        }

        public CreditCardTypeView GetCreditCardType(int id)
        {
            try
            {
                return new CreditCardsRepository().GetCreditCardTypeView(id);
            }
            catch
            {
                throw new FaultException("An error occurred whilst retrieving the credit card type details.");
            }
        }

        public void AddCreditCardType(string name)
        {
            try
            {
                new CreditCardsRepository().AddCreditCardType(new CreditCardType() { CreditCardType1 = name });
            }
            catch
            {
                throw new FaultException("An error occurred whilst adding the new credit card type.");
            }
        }

        public void UpdateCreditCardType(int id, string name)
        {
            try
            {
                new CreditCardsRepository().UpdateCreditCardType(new CreditCardType()
                {
                    Id = id,
                    CreditCardType1 = name
                });
            }
            catch
            {
                throw new FaultException("An error occurred whilst updating the credit card type.");
            }
        }

        public void DeleteCreditCardType(int id)
        {
            //may send notice to customers that the credit card is not supported any longer
            try
            {
                CreditCardsRepository ccr = new CreditCardsRepository();
                try
                {
                    ccr.Entity.Database.Connection.Open();
                    ccr.Transaction = ccr.Entity.Database.BeginTransaction();

                    ccr.DeleteCreditDetailsForCreditCardType(id);
                    ccr.DeleteCreditCardType(id);

                    ccr.Transaction.Commit();
                }
                catch
                {
                    ccr.Transaction.Rollback();
                    throw new TransactionFailedException("Credit Card Type Deletion Failed. Please try again or contact support.");
                }
                finally
                {
                    ccr.Entity.Database.Connection.Close();
                }

            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("An error occurred whilst deleting the credit card type.");
            }
        }
    }
}
