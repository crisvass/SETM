using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.CustomExceptions;
using System.Data.Entity;
using Common.Views;

namespace DataAccessLayer
{
    public class UsersRepository : ConnectionClass
    {
        public UsersRepository() : base() { }

        public ApplicationUser GetUser(string username)
        {
            return Entity.ApplicationUsers.SingleOrDefault(u => u.UserName == username);
        }

        public ApplicationUser GetUserById(string id)
        {
            ApplicationUser u = Entity.ApplicationUsers.SingleOrDefault(user => user.Id == id);
            return Entity.ApplicationUsers.SingleOrDefault(user => user.Id == id);
        }

        public Seller GetSeller(string userId)
        {
            return Entity.Sellers.SingleOrDefault(sel => sel.Id == userId);
        }

        //public bool DoesUsernameExist(string username)
        //{
        //    return GetUser(username) != null;
        //}

        //public bool DoesEmailExist(string email)
        //{
        //    return Entity.ApplicationUsers.SingleOrDefault(u => u.Email == email) != null;
        //}

        //public bool IsUserAuthenticated(string username, string password)//byte[] password)
        //{
        //    if (Entity.ApplicationUsers.SingleOrDefault(u => u.UserName == username && u.PasswordHash == password) != null)
        //        return true;
        //    else
        //    {
        //        if (GetUser(username) == null)
        //            throw new InvalidUsernameException();
        //        else
        //            throw new InvalidPasswordException();
        //    }
        //}

        public void AddUser(ApplicationUser u)
        {
            Entity.ApplicationUsers.Add(u);
            Entity.SaveChanges();
        }

        public void AddSeller(Seller s)
        {
            Entity.Sellers.Add(s);
            Entity.SaveChanges();
        }

        public void UpdateUser(ApplicationUser u)
        {
            ApplicationUser user = GetUserById(u.Id);
            user.FirstName = u.FirstName;
            user.LastName = u.LastName;
            user.Residence = u.Residence;
            user.Street = u.Street;
            user.Town = u.Town;
            user.PostCode = u.PostCode;
            user.Country = u.Country;
            Entity.SaveChanges();
        }

        public void UpdateSeller(Seller s)
        {
            Seller seller = GetSeller(s.Id);
            seller.RequiresDelivery = s.RequiresDelivery;
            seller.IBANNumber = s.IBANNumber;
            Entity.SaveChanges();
        }

        public void DeleteUser(string userId)
        {
            Entity.ApplicationUsers.Remove(GetUserById(userId));
            Entity.SaveChanges();
        }

        public void MarkUserAsDeleted(string userId)
        {
            ApplicationUser user = GetUserById(userId);
            user.IsDeleted = true;
            Entity.SaveChanges();
        }

        public void DeleteSeller(string userId)
        {
            Entity.Sellers.Remove(GetSeller(userId));
            Entity.SaveChanges();
        }

        public UserView GetUserView(string userId)
        {
            ApplicationUser u = GetUserById(userId);
            return new UserView()
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Residence = u.Residence,
                Street = u.Street,
                PostCode = u.PostCode,
                Town = u.Town,
                Country = u.Country,
                ContactNumber = u.ContactNumber
            };
        }

        public IEnumerable<UserView> GetAllUsers()
        {
            return Entity.ApplicationUsers.Where(u=> u.IsDeleted == false).Select(u =>
                new UserView()
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Residence = u.Residence,
                Street = u.Street,
                PostCode = u.PostCode,
                Town = u.Town,
                Country = u.Country,
                ContactNumber = u.ContactNumber
            });
        }
    }
}
