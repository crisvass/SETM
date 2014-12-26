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
            CreditCardsRepository ccr = new CreditCardsRepository();

            ur.Entity = rr.Entity = ccr.Entity;

            try
            {
                UserView user = ur.GetUserView(id);
                user.UserRoles = rr.GetUserRoles(user.Username).ToList<RoleView>();
                user.CreditCards = ccr.GetUserCreditCards(user.Username).ToList<CreditCardDetailView>();
                Seller s;
                if ((s = ur.GetSeller(id)) != null)
                {
                    user.RequiresDelivery = s.RequiresDelivery;
                    user.IbanNumber = s.IBANNumber;
                }
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

        public void AddUser(string username, string password, string email, string name, string surname, string residence,
            string street, string town, string postCode, string country, string contactNumber,
            List<CreditCardDetailView> creditCards, bool requiresDelivery, string ibanNumber, List<RoleView> userRoles)
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
                    UserName = username,
                    PasswordHash = GeneralUtitlities.HashPassword(password),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = email,
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

                    bool buyer = false;
                    bool seller = false;
                    foreach (RoleView rv in userRoles)
                    {
                        if (rv.RoleName.ToLower() == "buyer")
                            buyer = true;

                        if (rv.RoleName.ToLower() == "seller")
                            seller = true;

                        rr.AllocateRole(u, rv.RoleId);
                    }

                    if (buyer)
                    {
                        foreach (CreditCardDetailView ccd in creditCards)
                        {
                            ccr.AddCreditCardDetail(new CreditCardDetail()
                            {
                                Username = username,
                                CreditCardTypeId = ccd.CreditCardTypeId,
                                CreditCardNumber = ccd.CreditCardNumber,
                                CardHolderName = ccd.CardHolderName,
                                ExpiryDate = new DateTime(ccd.Year, ccd.Month, GeneralUtitlities.GetLastDayOfTheMonth(ccd.Month, ccd.Year))
                            });
                        }
                    }

                    if (seller)
                    {
                        ur.AddSeller(new Seller()
                        {
                            Id = userId.ToString(),
                            RequiresDelivery = requiresDelivery,
                            IBANNumber = ibanNumber
                        });
                    }

                    ur.Transaction.Commit();
                }
                catch
                {
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

        public void UpdateUser(string id, string email, string name, string surname, string residence,
            string street, string town, string postCode, string country, string contactNumber,
            List<CreditCardDetailView> creditCards, bool requiresDelivery, string ibanNumber, List<RoleView> userRoles)
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
                    Email = email,
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
                    ur.UpdateUser(u);

                    string username = ur.GetUserById(id).UserName;
                    bool buyer = false;
                    bool seller = false;
                    bool found;

                    IEnumerable<RoleView> userOriginalRoles = rr.GetUserRoles(username);

                    //delete removed user roles
                    foreach (RoleView rv in userOriginalRoles)
                    {
                        found = false;
                        foreach (RoleView role in userRoles)
                        {
                            if (rv.RoleId == role.RoleId)
                                found = true;
                        }

                        if (!found)
                            rr.DeleteUserRole(id, rv.RoleId);
                    }

                    //add new user roles
                    foreach (RoleView rv in userRoles)
                    {
                        if (rv.RoleName.ToLower() == "buyer")
                            buyer = true;

                        if (rv.RoleName.ToLower() == "seller")
                            seller = true;

                        found = false;
                        foreach (RoleView role in userOriginalRoles)
                        {
                            if (rv.RoleId == role.RoleId)
                                found = true;
                        }

                        if (!found)
                            rr.AllocateRole(u, id);
                    }

                    IEnumerable<CreditCardDetailView> creditCardsOriginal = ccr.GetUserCreditCards(username);
                    if (buyer)
                    {
                        //delete removed credit cards
                        foreach (CreditCardDetailView card in creditCardsOriginal)
                        {
                            found = false;
                            foreach (CreditCardDetailView ccd in creditCards)
                            {
                                if (card.Id == ccd.Id)
                                {
                                    found = true;
                                }
                            }

                            if (!found)
                            {
                                ccr.DeleteCreditCardDetail(card.Id);
                            }
                        }

                        //add new credit cards
                        foreach (CreditCardDetailView ccd in creditCards)
                        {
                            found = false;
                            foreach (CreditCardDetailView card in creditCardsOriginal)
                            {
                                if (ccd.Id == card.Id)
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                ccr.AddCreditCardDetail(new CreditCardDetail()
                                {
                                    Username = username,
                                    CreditCardTypeId = ccd.CreditCardTypeId,
                                    CreditCardNumber = ccd.CreditCardNumber,
                                    CardHolderName = ccd.CardHolderName,
                                    ExpiryDate = new DateTime(ccd.Year, ccd.Month, GeneralUtitlities.GetLastDayOfTheMonth(ccd.Month, ccd.Year))
                                });
                            }
                        }
                    }
                    else
                    {
                        ccr.DeleteUserCreditCards(username);
                    }

                    if (seller)
                    {
                        Seller s = new Seller()
                            {
                                Id = id,
                                RequiresDelivery = requiresDelivery,
                                IBANNumber = ibanNumber
                            };

                        if (ur.GetSeller(id) != null)
                        {
                            ur.UpdateSeller(s);
                        }
                        else
                        {
                            ur.AddSeller(s);
                        }
                    }
                    else
                    {
                        ur.DeleteSeller(id);
                    }

                    ur.Transaction.Commit();
                }
                catch
                {
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

        public void DeleteUser(string id)
        {
            try
            {
                UsersRepository ur = new UsersRepository();
                try
                {
                    ur.Entity.Database.Connection.Open();
                    ur.Transaction = ur.Entity.Database.BeginTransaction();

                    ur.MarkUserAsDeleted(id);
                    ur.Transaction.Commit();
                }
                catch
                {
                    ur.Transaction.Rollback();
                    throw new TransactionFailedException("User Deletion Failed. Please try again or contact administrator if error persists.");
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
                throw new FaultException("An error occurred whilst deleting the user.");
            }
        }
    }
}
