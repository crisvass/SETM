using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Views;

namespace DataAccessLayer
{
    public class CreditCardsRepository : ConnectionClass
    {
        public CreditCardsRepository() : base() { }

        public void AddCreditCardDetail(CreditCardDetail cc)
        {
            Entity.CreditCardDetails.Add(cc);
            Entity.SaveChanges();
        }

        public void UpdateCreditCardDetail(CreditCardDetail cc)
        {
            CreditCardDetail creditCard = Entity.CreditCardDetails.SingleOrDefault(ccd => ccd.Id == cc.Id);
            creditCard.CreditCardNumber = cc.CreditCardNumber;
            creditCard.ExpiryDate = cc.ExpiryDate;
            Entity.SaveChanges();
        }

        public void DeleteCreditCardDetail(int id)
        {
            Entity.CreditCardDetails.Remove(Entity.CreditCardDetails.SingleOrDefault(cc => cc.Id == id));
            Entity.SaveChanges();
        }

        public IEnumerable<CreditCardTypeView> GetCardTypes()
        {
            return Entity.CreditCardTypes
                .Select(cct => new CreditCardTypeView()
                {
                    CreditCardTypeId = cct.Id,
                    CreditCardType = cct.CreditCardType1
                });
        }
    }
}
