using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Common.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using TradersMarketplace.Models;
using TradersMarketplace.UsersServiceClient;

namespace TradersMarketplace.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserManagementController : BaseController
    {
        //private UserDbcontext db = new UserDbcontext();
        private UsersServiceClient.UsersServiceClient us = new UsersServiceClient.UsersServiceClient();

        // GET: UserManagement
        public ActionResult Index()
        {
            List<UserView> usersview = us.GetAllUsers();
            List<UserView> users = new List<UserView>();
            foreach (UserView u in usersview)
            {
                UserView user = u;
                user.UserRolesList = new SelectList(user.UserRoles,
                    "RoleId", "RoleName");
                users.Add(user);
            }

            return View(users);
        }

        // GET: UserManagement/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserView user = new UserView();
            try
            {
                user = us.GetUser(id);
                user.UserRolesList = new SelectList(user.UserRoles,
                    "RoleId", "RoleName");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(user);
        }

        // GET: UserManagement/Create
        public ActionResult Create()
        {
            UserView uv = new UserView();
            uv.UserRolesList = new SelectList(new RolesServiceClient.RolesServiceClient().GetRoles(), "RoleId", "RoleName");
            return View(uv);
        }

        // POST: UserManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(string username, string password, string email, string firstName, string lastName,
            string contactNumber, string residence, string street, string postCode, string town, string country,
            string creditCards, bool requiresDelivery, string ibanNumber, string userRoles)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<CreditCardDetailView> creditCardsList = js.Deserialize<List<CreditCardDetailView>>(creditCards);
            List<RoleView> userRolesList = js.Deserialize<List<RoleView>>(userRoles);

            UserView user = new UserView()
            {
                Username = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                ContactNumber = contactNumber,
                Residence = residence,
                Street = street,
                Town = town,
                PostCode = postCode,
                Country = country,
                CreditCards = creditCardsList,
                RequiresDelivery = requiresDelivery,
                IbanNumber = ibanNumber,
                UserRoles = userRolesList
            };

            if (ModelState.IsValid)
            {
                try
                {
                    us.AddUser(username, password,
                        email, firstName, lastName, residence, street, town, postCode,
                        country, contactNumber, creditCardsList, requiresDelivery, ibanNumber, userRolesList);
                    return Json(Url.Action("Index", "UserManagement"));
                }
                catch (FaultException ex)
                {
                    throw ex;
                }
            }
            return View(user);
        }

        [HttpGet]
        public PartialViewResult CreateCreditCard()
        {
            CreditCardDetailView view = new CreditCardDetailView();
            try
            {
                view.CreditCardTypeList = new SelectList(new CreditCardsServiceClient.CreditCardsServiceClient().GetCreditCardTypes(),
                    "CreditCardTypeId", "CreditCardType");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            List<MonthModel> months = new List<MonthModel>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(new MonthModel() { MonthNum = i, Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i) });
            }
            string[] years = new string[5];

            for (int i = 0; i < years.Length; i++)
            {
                years[i] = (DateTime.Now.Year + i).ToString();
            }

            view.MonthsList = new SelectList(months, "MonthNum", "Month");
            view.YearsList = new SelectList(years);

            return PartialView("_CreateCreditCard", view);
        }

        //// GET: UserManagement/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    UserCrud userCrud = db.Users.Find(id);
        //    if (userCrud == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(userCrud);
        //}

        //// POST: UserManagement/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Username,Password,Email,FirstName,LastName,ContactNumber,Residence,Street,PostCode,Town,Country")] UserCrud userCrud)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(userCrud).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(userCrud);
        //}

        // GET: UserManagement/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserView user = null;
            try
            {
                user = us.GetUser(id);
                user.UserRolesList = new SelectList(user.UserRoles,
                    "RoleId", "RoleName");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(user);
        }

        // POST: UserManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                us.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            UserView user = us.GetUser(id);
            user.UserRolesList = new SelectList(user.UserRoles,
                "RoleId", "RoleName");
            return View(user);
        }
    }
}
