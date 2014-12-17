using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Common.CustomExceptions;
using TradersMarketplace.Models;
using TradersMarketplace.RolesServiceClient;

namespace TradersMarketplace.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class RoleCrudsController : BaseController
    {
        private RolesServiceClient.RolesServiceClient rs = new RolesServiceClient.RolesServiceClient();

        // GET: RoleCruds
        public ActionResult Index()
        {
            return View(rs.GetRoles());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoleView role = null;
            try
            {
                role = rs.GetRole(id);
            }
            catch(FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(role);
        }

        // GET: RoleCruds/Create
        public ActionResult Create()
        {
            return View(new RoleView());
        }

        // POST: RoleCruds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleView role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    rs.AddRole(role.RoleName);
                    return RedirectToAction("Index");
                }
                catch (FaultException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(role);
        }

        // GET: RoleCruds/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            RoleView role = null;
            try
            {
                role = rs.GetRole(id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(role);
        }

        // POST: RoleCruds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleView role)
        {
            try
            {
                rs.UpdateRole(role.RoleId, role.RoleName);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(role);
        }

        // GET: RoleCruds/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RoleView role = null;
            try
            {
                role = rs.GetRole(id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(role);
        }

        // POST: RoleCruds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                rs.DeleteRole(id);
                return RedirectToAction("Index");
            }
            catch (TransactionFailedException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (ConstraintException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(rs.GetRole(id));
        }
    }
}
