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

        #region Credit Card Details

        public void AddCreditCardDetail(CreditCardDetail cc)
        {
            try
            {
                Entity.CreditCardDetails.Add(cc);
                Entity.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void DeleteCreditCardDetail(int id)
        {
            Entity.CreditCardDetails.Remove(Entity.CreditCardDetails.SingleOrDefault(cc => cc.Id == id));
            Entity.SaveChanges();
        }

        public IEnumerable<CreditCardDetailView> GetUserCreditCards(string userId)
        {
            return Entity.CreditCardDetails.Where(cc => cc.UserId == userId)
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

        public IEnumerable<CreditCardDetailView> GetUserNonExpiredCreditCards(string userId)
        {
            return GetUserCreditCards(userId).Where(cc => cc.ExpiryDate >= DateTime.Now);
        }

        public void DeleteUserCreditCards(string userId)
        {
            IEnumerable<CreditCardDetail> creditCards = Entity.CreditCardDetails.Where(cc => cc.UserId == userId);
            foreach (CreditCardDetail cc in creditCards)
            {
                Entity.CreditCardDetails.Remove(cc);
            }
            Entity.SaveChanges();
        }

        #endregion

        #region Credit Card Types

        public CreditCardType GetCreditCardType(int id)
        {
            return Entity.CreditCardTypes.SingleOrDefault(cct => cct.Id == id);
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

        public CreditCardTypeView GetCreditCardTypeView(int id)
        {
            return Entity.CreditCardTypes.Select(cct => new CreditCardTypeView()
            {
                CreditCardTypeId = cct.Id,
                CreditCardType = cct.CreditCardType1
            }).SingleOrDefault(cct => cct.CreditCardTypeId == id); ;
        }

        public void AddCreditCardType(CreditCardType cct)
        {
            Entity.CreditCardTypes.Add(cct);
            Entity.SaveChanges();
        }

        public void UpdateCreditCardType(CreditCardType cct)
        {
            CreditCardType creditCardType = GetCreditCardType(cct.Id);
            creditCardType.CreditCardType1 = cct.CreditCardType1;
            Entity.SaveChanges();
        }

        public void DeleteCreditCardType(int id)
        {
            Entity.CreditCardTypes.Remove(GetCreditCardType(id));
            Entity.SaveChanges();
        }

        public void DeleteCreditDetailsForCreditCardType(int id)
        {
            IEnumerable<CreditCardDetail> details = Entity.CreditCardDetails.Where(ccd => ccd.CreditCardTypeId == id);
            foreach (CreditCardDetail ccd in details)
            {
                Entity.CreditCardDetails.Remove(ccd);
            }
            Entity.SaveChanges();
        }

        #endregion
    }
}
