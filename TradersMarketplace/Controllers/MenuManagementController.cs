using System;
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
        [ValidateAntiForgeryToken]
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
                    return RedirectToAction("Index");
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

        //// GET: MenuManagement/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MenusCrud menusCrud = db.Menus.Find(id);
        //    if (menusCrud == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(menusCrud);
        //}

        //// POST: MenuManagement/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MenuId,Action,Url,Title,Position,HasSubmenus")] MenusCrud menusCrud)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(menusCrud).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(menusCrud);
        //}

        //// GET: MenuManagement/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MenusCrud menusCrud = db.Menus.Find(id);
        //    if (menusCrud == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(menusCrud);
        //}

        //// POST: MenuManagement/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MenusCrud menusCrud = db.Menus.Find(id);
        //    db.Menus.Remove(menusCrud);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}        
    }
}
