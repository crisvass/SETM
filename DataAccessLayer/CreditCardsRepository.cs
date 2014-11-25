using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccessLayer
{
    public class CreditCardsRepository: ConnectionClass
    {
        public CreditCardsRepository() : base() { }

        public void AddCreditCardDetail(CreditCardDetail cc)
        {
            Entity.CreditCardDetails.Add(cc);
            Entity.SaveChanges();
        }        
    }
}
