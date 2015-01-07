using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using Common;
using Common.CustomExceptions;
using Common.Views;
using DataAccessLayer;

namespace BusinessLayer_WebServices
{
    public class RolesService : IRolesService
    {
        public IEnumerable<RoleView> GetUserRoles(string username)
        {
            try
            {
                return new RolesRepository().GetUserRoles(username);
            }
            catch
            {
                throw new FaultException("An error occurred whilst retrieving the user roles.");
            }
        }

        public IEnumerable<RoleView> GetRoles()
        {
            try
            {
                return new RolesRepository().GetRoles();
            }
            catch
            {
                throw new FaultException("An error occurred whilst retrieving the list of roles.");
            }
        }

        public IdentityRole AddRole(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (name.Length >= 1 && name.Length <= 25)
                    {
                        Regex regexItem = new Regex("[a-zA-Z]$");
                        if (regexItem.IsMatch(name))
                        {
                            RolesRepository rr = new RolesRepository();
                            if (rr.GetRoles().SingleOrDefault(r => r.RoleName == name) == null)
                            {
                                return rr.AddRole(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = name });
                            }
                            else
                            {
                                throw new ConstraintException("Role name must be unique.");
                            }
                        }
                        else
                        {
                            throw new ArgumentException("Role name can contain only alphabet letters.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Role name must be between 1 and 25 characters long.");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Role name cannot be null or empty.");
                }
            }
            catch (ConstraintException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("An error occurred whilst adding the new role.");
            }
        }

        public IdentityRole UpdateRole(string id, string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Guid roleId = Guid.Parse(id);
                    if (roleId != Guid.Empty)
                    {
                        RolesRepository rr = new RolesRepository();
                        IEnumerable<RoleView> roles = rr.GetRoles();

                        if (roles.SingleOrDefault(r => r.RoleId == id) != null)
                        {
                            if (!string.IsNullOrEmpty(name))
                            {
                                if (name.Length >= 1 && name.Length <= 25)
                                {
                                    Regex regexItem = new Regex("[a-zA-Z]$");
                                    if (regexItem.IsMatch(name))
                                    {
                                        if (roles.SingleOrDefault(r => r.RoleName == name) == null)
                                        {
                                            return rr.UpdateRole(new IdentityRole() { Id = id, Name = name });
                                        }
                                        else
                                        {
                                            throw new ConstraintException("Role name must be unique.");
                                        }
                                    }
                                    else
                                    {
                                        throw new ArgumentException("Role name can contain only alphabet letters.");
                                    }
                                }
                                else
                                {
                                    throw new ArgumentException("Role name must be between 1 and 25 characters long.");
                                }
                            }
                            else
                            {
                                throw new ArgumentNullException("Role name cannot be null or empty.");
                            }
                        }
                        else
                        {
                            throw new ConstraintException("Role ID does not exist.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Role ID cannot be an empty GUID.");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Role ID cannot be null or empty.");
                }
            }
            catch (FormatException ex)
            {
                throw new FaultException("Role ID was not a valid GUID value.");
            }
            catch (ConstraintException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("An error occurred whilst adding the new role.");
            }
        }

        public RoleView GetRole(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Guid roleId = Guid.Parse(id);
                    if (roleId != Guid.Empty)
                    {
                        RolesRepository rr = new RolesRepository();
                        IEnumerable<RoleView> roles = rr.GetRoles();

                        if (roles.SingleOrDefault(r => r.RoleId == id) != null)
                        {
                            return rr.GetRoleView(id);
                        }
                        else
                        {
                            throw new ConstraintException("Role ID does not exist.");
                        }

                    }
                    else
                    {
                        throw new ArgumentException("Role ID cannot be an empty GUID.");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Role ID cannot be null or empty.");
                }
            }
            catch (FormatException ex)
            {
                throw new FaultException("Role ID was not a valid GUID value.");
            }
            catch (ConstraintException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("An error occurred whilst adding the new role.");
            }
        }

        public void DeleteRole(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Guid roleId = Guid.Parse(id);
                    if (roleId != Guid.Empty)
                    {
                        RolesRepository rr = new RolesRepository();
                        IEnumerable<RoleView> roles = rr.GetRoles();

                        if (roles.SingleOrDefault(r => r.RoleId == id) != null)
                        {
                            if (!rr.RoleIsAssigned(id))
                            {
                                try
                                {
                                    rr.Entity.Database.Connection.Open();
                                    rr.Transaction = rr.Entity.Database.BeginTransaction();

                                    rr.DeleteMenuRoles(id);
                                    rr.DeleteRole(id);

                                    rr.Transaction.Commit();
                                }
                                catch
                                {
                                    rr.Transaction.Rollback();
                                    throw new TransactionFailedException("Role Deletion Failed. Please try again or contact administrator if error persists.");
                                }
                                finally
                                {
                                    rr.Entity.Database.Connection.Close();
                                }
                            }
                            else
                            {
                                throw new ConstraintException("Role cannot be deleted because it is assigned to a user.");
                            }
                        }
                        else
                        {
                            throw new ConstraintException("Role ID does not exist.");
                        }

                    }
                    else
                    {
                        throw new ArgumentException("Role ID cannot be an empty GUID.");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Role ID cannot be null or empty.");
                }
            }
            catch (FormatException ex)
            {
                throw new FaultException("Role ID was not a valid GUID value.");
            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ConstraintException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("An error occurred whilst adding the new role.");
            }            
        }

        public IEnumerable<RoleView> GetNonMenuAssignedRoles(Guid id)
        {
            try
            {
                return new RolesRepository().GetNonMenuAssignedRoles(id);
            }
            catch
            {
                throw new FaultException("An error occurred whilst retrieving the list of roles.");
            }
        }

        public IEnumerable<RoleView> GetNonUserAssignedRoles(String id)
        {
            try
            {
                return new RolesRepository().GetNonUserAssignedRoles(id);
            }
            catch
            {
                throw new FaultException("An error occurred whilst retrieving the list of roles.");
            }
        }
    }
}
