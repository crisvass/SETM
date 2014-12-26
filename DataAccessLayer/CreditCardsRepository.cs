using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            //try
            //{
                Entity.CreditCardDetails.Add(cc);
                Entity.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
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

        public IEnumerable<CreditCardDetailView> GetUserCreditCards(string username)
        {
            return Entity.CreditCardDetails.Where(cc => cc.Username == username)
                .Select(cc => new CreditCardDetailView()
                {
                    Id = cc.Id,
                    CreditCardTypeId = cc.CreditCardTypeId,
                    CreditCardType = cc.CreditCardType.CreditCardType1,
                    CardHolderName = cc.CardHolderName,
                    CreditCardNumber = cc.CreditCardNumber,
                    ExpiryDate = cc.ExpiryDate
                });
        }

        public void DeleteUserCreditCards(string username)
        {
            IEnumerable<CreditCardDetail> creditCards = Entity.CreditCardDetails.Where(cc => cc.Username == username);
            foreach (CreditCardDetail cc in creditCards)
            {
                Entity.CreditCardDetails.Remove(cc);
            }
            Entity.SaveChanges();
        }
    }
}
