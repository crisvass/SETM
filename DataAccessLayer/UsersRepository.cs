using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.CustomExceptions;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class UsersRepository : ConnectionClass
    {
        public UsersRepository() : base() { }

        public ApplicationUser GetUser(string username)
        {
            return Entity.ApplicationUsers.SingleOrDefault(u => u.UserName == username);
        }

        public void AddUser(ApplicationUser u)
        {
                Entity.ApplicationUsers.Add(u);
                Entity.SaveChanges();
        }

        public void AddSeller(ApplicationUser u, Seller s)
        {
            u.Seller = s;
            Entity.SaveChanges();
        }

        public bool DoesUsernameExist(string username)
        {
            return GetUser(username) != null;
        }

        public bool DoesEmailExist(string email)
        {
            return Entity.ApplicationUsers.SingleOrDefault(u => u.Email == email) != null;
        }

        public bool IsUserAuthenticated(string username, string password)//byte[] password)
        {
            if (Entity.ApplicationUsers.SingleOrDefault(u => u.UserName == username && u.PasswordHash == password) != null)
                return true;
            else
            {
                if (GetUser(username) == null)
                    throw new InvalidUsernameException();
                else
                    throw new InvalidPasswordException();
            }
        }
    }
}
