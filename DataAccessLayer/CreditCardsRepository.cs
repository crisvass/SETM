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
