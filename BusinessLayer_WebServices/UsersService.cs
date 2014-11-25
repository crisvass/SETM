using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using Common.CustomExceptions;
using Common.Utilities;
using DataAccessLayer;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UsersService" in both code and config file together.
    public class UsersService : IUsersService
    {
        public void RegisterBuyer(string username, string password, string email,
            string name, string surname, string residence, string street, string town,
            string postCode, string country, int creditCardTypeId, string cardHolderName,
            int expiryDateMonth, int expiryDateYear)
        {
            UsersRepository ur = new UsersRepository();
            RolesRepository rr = new RolesRepository();
            CreditCardsRepository ccr = new CreditCardsRepository();
            ur.Entity = rr.Entity = ccr.Entity;

            try
            {
                if (!ur.DoesEmailExist(email))
                {
                    if (!ur.DoesUsernameExist(username))
                    {
                        User u = new User()
                        {
                            Username = username,
                            Password = password,
                            Email = email,
                            Name = name,
                            Surname = surname,
                            Residence = residence,
                            Street = street,
                            Town = town,
                            PostCode = postCode,
                            Country = country
                        };

                        CreditCardDetail cc = new CreditCardDetail()
                        {
                            CreditCardTypeId = creditCardTypeId,
                            CardHolderName = cardHolderName,
                            ExpiryDate = new DateTime(expiryDateYear, expiryDateMonth,
                                GeneralUtitlities.GetLastDayOfTheMonth(expiryDateMonth, expiryDateYear))

                        };

                        try
                        {
                            ur.Entity.Database.Connection.Open();
                            rr.Transaction = ur.Transaction = ccr.Transaction = ur.Entity.Database.BeginTransaction();
                            ur.AddUser(u);
                            ccr.AddCreditCardDetail(cc);
                            rr.AllocateRole(u, rr.GetBuyerRole());
                            ur.Transaction.Commit();
                        }
                        catch
                        {
                            ur.Transaction.Rollback();
                            throw new TransactionFailedException("Registration Failed. Please try again or contact administrator if error persists.");
                        }
                        finally
                        {
                            ur.Entity.Database.Connection.Close();
                        }
                    }
                }
            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("Error whilst processing registration. Please try again or contact administrator if error persists.");
            }
        }


        public void RegisterSeller(string username, string password, string email, string name, string surname, string residence, string street, string town, string postCode, string country, bool requiresDelivery, string ibanNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsUserAuthenticated(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
