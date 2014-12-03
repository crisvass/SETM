using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using Common;
using Common.CustomExceptions;
using Common.Utilities;
using DataAccessLayer;

namespace BusinessLayer_WebServices
{
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
                        var data = Encoding.UTF8.GetBytes(password);
                        byte[] hashedPassword;
                        using (SHA512 shaM = new SHA512Managed())
                        {
                            hashedPassword = shaM.ComputeHash(data);
                        }

                        User u = new User()
                        {
                            Username = username,
                            Password = hashedPassword,
                            Email = email,
                            Name = name,
                            Surname = surname,
                            Residence = residence,
                            Street = street,
                            Town = town,
                            PostCode = postCode,
                            Country = country
                        };

                        CreditCardDetail cc = null;
                        if (creditCardTypeId != null && cardHolderName != null && expiryDateMonth != null && expiryDateYear != null)
                        {
                            cc = new CreditCardDetail()
                            {
                                CreditCardTypeId = creditCardTypeId,
                                CardHolderName = cardHolderName,
                                ExpiryDate = new DateTime(expiryDateYear, expiryDateMonth,
                                    GeneralUtitlities.GetLastDayOfTheMonth(expiryDateMonth, expiryDateYear))

                            };
                        }

                        try
                        {
                            ur.Entity.Database.Connection.Open();
                            rr.Transaction = ur.Transaction = ccr.Transaction = ur.Entity.Database.BeginTransaction();
                            ur.AddUser(u);
                            if (cc != null)
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
            UsersRepository ur = new UsersRepository();
            RolesRepository rr = new RolesRepository();
            
            ur.Entity = rr.Entity;

            try
            {
                if (!ur.DoesEmailExist(email))
                {
                    if (!ur.DoesUsernameExist(username))
                    {
                        var data = Encoding.UTF8.GetBytes(password);
                        byte[] hashedPassword;
                        using (SHA512 shaM = new SHA512Managed())
                        {
                            hashedPassword = shaM.ComputeHash(data);
                        }

                        User u = new User()
                        {
                            Username = username,
                            Password = hashedPassword,
                            Email = email,
                            Name = name,
                            Surname = surname,
                            Residence = residence,
                            Street = street,
                            Town = town,
                            PostCode = postCode,
                            Country = country                           
                        };

                        Seller s = new Seller()
                        {
                            IBANNumber = ibanNumber,
                            RequiresDelivery = requiresDelivery
                        };
                        
                        try
                        {
                            ur.Entity.Database.Connection.Open();
                            rr.Transaction = ur.Transaction = ur.Entity.Database.BeginTransaction();
                            ur.AddUser(u);
                            ur.AddSeller(u, s);
                            rr.AllocateRole(u, rr.GetSellerRole());
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

        public bool IsUserAuthenticated(string username, string password)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(password);
                byte[] hashedPassword;
                using (SHA512 shaM = new SHA512Managed())
                {
                    hashedPassword = shaM.ComputeHash(data);
                }

                return new UsersRepository().IsUserAuthenticated(username, hashedPassword);
            }
            catch (InvalidUsernameException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("Error whilst authenticating your username and password. Please try again or contact administrator if error persists.");
            }
        }
    }
}
