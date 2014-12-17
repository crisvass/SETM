using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using Common.CustomExceptions;
using Common.Views;
using DataAccessLayer;

namespace BusinessLayer_WebServices
{
    public class RolesService : IRolesService
    {
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

        public void AddRole(string name)
        {
            try
            {
                new RolesRepository().AddRole(new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = name });
            }
            catch
            {
                throw new FaultException("An error occurred whilst adding the new role.");
            }
        }

        public void UpdateRole(string id, string name)
        {
            try
            {
                new RolesRepository().UpdateRole(new IdentityRole() { Id = id, Name = name });
            }
            catch
            {
                throw new FaultException("An error occurred whilst updating role.");
            }
        }

        public RoleView GetRole(string id)
        {
            try
            {
                return new RolesRepository().GetRole(id);
            }

            catch
            {
                throw new FaultException("An error occurred whilst getting the role's details.");
            }
        }

        public void DeleteRole(string id)
        {
            try
            {
                RolesRepository rr = new RolesRepository();
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
            catch (TransactionFailedException ex)
            {
                throw ex;
            }
            catch (ConstraintException ex)
            {
                throw ex;
            }
            catch
            {
                throw new FaultException("An error occurred whilst deleting the role.");
            }
        }
    }
}
