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
using Common.Views;
using DataAccessLayer;

namespace BusinessLayer_WebServices
{
    public class UsersService : IUsersService
    {
        public void RegisterBuyer(string id, string name, string surname, string residence, string street,
            string town, string postCode, string country, string contactNumber, int creditCardTypeId, 
            string creditCardNumber, string cardHolderName, int expiryDateMonth, int expiryDateYear)
        {
            UsersRepository ur = new UsersRepository();
            RolesRepository rr = new RolesRepository();
            CreditCardsRepository ccr = new CreditCardsRepository();
            ur.Entity = rr.Entity = ccr.Entity;

            try
            {
                ApplicationUser u = new ApplicationUser()
                    {
                        Id = id,
                        FirstName = name,
                        LastName = surname,
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
                    ur.UpdateUser(u);
                    if (cc != null)
                        ccr.AddCreditCardDetail(cc);
                    ur.Transaction.Commit();
                }
                catch
                {
                    ur.DeleteUser(id);
                    ur.Transaction.Rollback();
                    throw new TransactionFailedException("Registration Failed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    ur.Entity.Database.Connection.Close();
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


        public void RegisterSeller(string id, string name, string surname, string residence, string street, string town,
            string postCode, string country, string contactNumber, bool requiresDelivery, string ibanNumber)
        {
            UsersRepository ur = new UsersRepository();
            RolesRepository rr = new RolesRepository();

            ur.Entity = rr.Entity;

            try
            {

                ApplicationUser u = new ApplicationUser()
                {
                    Id = id,
                    FirstName = name,
                    LastName = surname,
                    Residence = residence,
                    Street = street,
                    Town = town,
                    PostCode = postCode,
                    Country = country
                };

                Seller s = new Seller()
                {
                    Id = id,
                    IBANNumber = ibanNumber,
                    RequiresDelivery = requiresDelivery
                };

                try
                {
                    ur.Entity.Database.Connection.Open();
                    rr.Transaction = ur.Transaction = ur.Entity.Database.BeginTransaction();
                    ur.UpdateUser(u);
                    ur.AddSeller(s);
                    ur.Transaction.Commit();
                }
                catch
                {
                    ur.DeleteUser(id);
                    ur.Transaction.Rollback();
                    throw new TransactionFailedException("Registration Failed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    ur.Entity.Database.Connection.Close();
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

        //public bool IsUserAuthenticated(string username, string password)
        //{
        //    try
        //    {
        //        return new UsersRepository().IsUserAuthenticated(username, password);
        //    }
        //    catch (InvalidUsernameException ex)
        //    {
        //        throw new FaultException(ex.Message);
        //    }
        //    catch (InvalidPasswordException ex)
        //    {
        //        throw new FaultException(ex.Message);
        //    }
        //    catch
        //    {
        //        throw new FaultException("Error whilst authenticating your username and password. Please try again or contact administrator if error persists.");
        //    }
        //}


        public UserView GetUser(string id)
        {
            UsersRepository ur = new UsersRepository();
            RolesRepository rr = new RolesRepository();
            
            ur.Entity = rr.Entity;

            try
            {
                UserView user = ur.GetUserView(id);
                user.UserRoles = rr.GetUserRoles(user.Username).ToList<RoleView>();
                return user;
            }
            catch
            {
                throw new FaultException("Error whilst retrieving user details. Please try again or contact administrator if error persists.");
            }
        }

        public IEnumerable<UserView> GetAllUsers()
        {
            UsersRepository ur = new UsersRepository();
            RolesRepository rr = new RolesRepository();

            ur.Entity = rr.Entity;

            try
            {
                List<UserView> users = ur.GetAllUsers().ToList<UserView>();
                List<UserView> usersWithRoles = new List<UserView>();

                foreach (UserView u in users)
                {
                    UserView uv = u;
                    uv.UserRoles = rr.GetUserRoles(u.Username).ToList<RoleView>();
                    usersWithRoles.Add(uv);
                }
                return usersWithRoles;
            }
            catch
            {
                throw new FaultException("Error whilst retrieving user details. Please try again or contact administrator if error persists.");
            }
        }

        public void AddBuyer(string id, string name, string surname, string residence, string street, string town, 
            string postCode, string country, string contactNumber, List<CreditCardDetailView> creditCards)
        {
            UsersRepository ur = new UsersRepository();
            RolesRepository rr = new RolesRepository();
            CreditCardsRepository ccr = new CreditCardsRepository();
            ur.Entity = rr.Entity = ccr.Entity;
            Guid userId = Guid.NewGuid();

            try
            {
                ApplicationUser u = new ApplicationUser()
                {
                    Id= userId.ToString(),
                    FirstName = name,
                    LastName = surname,
                    ContactNumber = contactNumber,
                    Residence = residence,
                    Street = street,
                    Town = town,
                    PostCode = postCode,
                    Country = country
                };               

                try
                {
                    ur.Entity.Database.Connection.Open();
                    rr.Transaction = ur.Transaction = ccr.Transaction = ur.Entity.Database.BeginTransaction();
                    ur.AddUser(u);

                    foreach (CreditCardDetailView ccd in creditCards)
                    {
                        ccr.AddCreditCardDetail(new CreditCardDetail()
                        {
                            CreditCardTypeId = ccd.CreditCardTypeId,
                            CardHolderName = ccd.CardHolderName,
                            ExpiryDate = ccd.ExpiryDate
                        });
                    }

                    rr.AllocateRole(u, rr.GetBuyerRole());

                    ur.Transaction.Commit();
                }
                catch
                {
                    ur.DeleteUser(id);
                    ur.Transaction.Rollback();
                    throw new TransactionFailedException("Adding a new user failed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    ur.Entity.Database.Connection.Close();
                }
            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("Error whilst adding a new user. Please try again or contact administrator if error persists.");
            }
        }

        public void AddSeller(string id, string name, string surname, string residence, string street, string town, 
            string postCode, string country, string contactNumber, bool requiresDelivery, string ibanNumber)
        {
            UsersRepository ur = new UsersRepository();
            RolesRepository rr = new RolesRepository();
            CreditCardsRepository ccr = new CreditCardsRepository();
            ur.Entity = rr.Entity = ccr.Entity;
            Guid userId = Guid.NewGuid();

            try
            {
                ApplicationUser u = new ApplicationUser()
                {
                    Id = userId.ToString(),
                    FirstName = name,
                    LastName = surname,
                    ContactNumber = contactNumber,
                    Residence = residence,
                    Street = street,
                    Town = town,
                    PostCode = postCode,
                    Country = country
                };

                try
                {
                    ur.Entity.Database.Connection.Open();
                    rr.Transaction = ur.Transaction = ccr.Transaction = ur.Entity.Database.BeginTransaction();
                    ur.AddUser(u);

                    ur.AddSeller(new Seller() { 
                        Id = userId.ToString(), 
                        RequiresDelivery = requiresDelivery, 
                        IBANNumber = ibanNumber
                    });

                    rr.AllocateRole(u, rr.GetSellerRole());

                    ur.Transaction.Commit();
                }
                catch
                {
                    ur.DeleteUser(id);
                    ur.Transaction.Rollback();
                    throw new TransactionFailedException("Adding a new user failed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    ur.Entity.Database.Connection.Close();
                }
            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("Error whilst adding a new user. Please try again or contact administrator if error persists.");
            }
        }
    }
}
