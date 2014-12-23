using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
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
            List<UserView> us = us.GetAllUsers();
            List<UserView> users = new List<UserView>();
            foreach (UserView u in us)
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

        //// GET: UserManagement/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: UserManagement/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Username,Password,Email,FirstName,LastName,ContactNumber,Residence,Street,PostCode,Town,Country")] UserCrud userCrud)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(userCrud);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(userCrud);
        //}

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

        //// GET: UserManagement/Delete/5
        //public ActionResult Delete(int? id)
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

        //// POST: UserManagement/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    UserCrud userCrud = db.Users.Find(id);
        //    db.Users.Remove(userCrud);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
