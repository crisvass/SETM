﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Models;
using TradersMarketplace.MenusServiceClient;
using System.ServiceModel;
using System.Web.Script.Serialization;

namespace TradersMarketplace.Controllers
{
    [Authorize(Roles="Administrator")]
    public class MenuManagementController : BaseController
    {
        private MenusServiceClient.MenusServiceClient ms = new MenusServiceClient.MenusServiceClient();

        // GET: MenuManagement
        public ActionResult Index()
        {
            return View(ms.GetAllMainMenus());
        }

        // GET: MenuManagement/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MenusView menu = null;
            try
            {
                menu = ms.GetMenu(id);
                menu.MenuRolesList = new SelectList(menu.MenuRoles, "RoleId", "RoleName");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(menu);
        }

        // GET: MenuManagement/Create
        public ActionResult Create()
        {
            MenusView mv = new MenusView()
            {
                MenuRolesList = new SelectList(
                    new RolesServiceClient.RolesServiceClient().GetRoles(),
                    "RoleId", "RoleName")
            };
            return View(mv);
        }

        // POST: MenuManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(string title, string action, string url, string submenus, string menuRoles)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<MenusView> submenusList = js.Deserialize<List<MenusView>>(submenus);
            List<RoleView> menuRolesList = js.Deserialize<List<RoleView>>(menuRoles);

            MenusView menu = new MenusView()
            {
                Title = title,
                Action = action,
                Url = url,
                Submenus = submenusList,
                MenuRoles = menuRolesList
            };
            if (ModelState.IsValid)
            {
                try
                {
                    ms.AddMenu(menu.Title, menu.Action, menu.Url, menu.Submenus, menu.MenuRoles);
                    return Json(Url.Action("Index", "MenuManagement"));
                    //return RedirectToAction("Index");
                }
                catch (FaultException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(menu);
        }

        [HttpGet]
        public PartialViewResult CreateSubmenu(MenusView menu)
        {
            return PartialView("_CreateSubmenu", new MenusView());
        }

        // GET: MenuManagement/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenusView menu = new MenusView();
            try
            {
                menu = ms.GetMenu(id);
                menu.MenuRolesList = new SelectList(
                        new RolesServiceClient.RolesServiceClient().GetNonMenuAssignedRoles(id),
                        "RoleId", "RoleName");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(menu);
        }

        // POST: MenuManagement/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, string title, string action, string url, string submenus, string menuRoles)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<MenusView> submenusList = js.Deserialize<List<MenusView>>(submenus);
            List<RoleView> menuRolesList = js.Deserialize<List<RoleView>>(menuRoles);

            MenusView menu = new MenusView()
            {
                MenuId = id,
                Title = title,
                Action = action,
                Url = url,
                Submenus = submenusList,
                MenuRoles = menuRolesList
            };

            try
            {
                ms.UpdateMenu(menu.MenuId, menu.Title, menu.Action, menu.Url, menu.Submenus, menu.MenuRoles);
                return Json(Url.Action("Index", "MenuManagement"));
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(menu);
        }

        // GET: MenuManagement/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenusView menu = new MenusView();
            try
            {
                menu = ms.GetMenu(id);
                menu.MenuRolesList = new SelectList(
                        new RolesServiceClient.RolesServiceClient().GetNonMenuAssignedRoles(id),
                        "RoleId", "RoleName");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(menu);
        }

        // POST: MenuManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                ms.DeleteMenu(id);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(ms.GetMenu(id).MenuRolesList = new SelectList(
                    new RolesServiceClient.RolesServiceClient().GetNonMenuAssignedRoles(id),
                    "RoleId", "RoleName"));
        }
    }
}