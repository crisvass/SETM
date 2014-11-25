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

        public User GetUser(string username)
        {
            return Entity.Users.SingleOrDefault(u => u.Username == username);
        }

        public void AddUser(User u)
        {
            Entity.Users.Add(u);
            Entity.SaveChanges();
        }

        public bool DoesUsernameExist(string username)
        {
            return GetUser(username) != null;
        }

        public bool DoesEmailExist(string email)
        {
            return Entity.Users.SingleOrDefault(u => u.Email == email) != null;
        }

        public bool IsUserAuthenticated(string username, string password)
        {
            if (Entity.Users.SingleOrDefault(u => u.Username == username && u.Password == password) != null)
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
