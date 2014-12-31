using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Models;
using Common.Views;
using System.ServiceModel;
using System.Web.Script.Serialization;

namespace TradersMarketplace.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductCategoryManagementController : BaseController
    {
        private ProductCategoriesServiceClient.ProductCategoriesServiceClient pcs =
            new ProductCategoriesServiceClient.ProductCategoriesServiceClient();

        // GET: ProductCategoryManagement
        public ActionResult Index()
        {
            IEnumerable<CategoryView> categories = null;
            try
            {
                categories = pcs.GetCategories();
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(categories);
        }

        // GET: ProductCategoryManagement/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryView category = null;
            try
            {
                category = pcs.GetCategory(id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(category);
        }

        // GET: ProductCategoryManagement/Create
        public ActionResult Create()
        {
            return View(new CategoryView());
        }

        // POST: ProductCategoryManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string categoryName, string subCategories)
        {
            CategoryView category = null;
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<CategoryView> subCategoriesList = js.Deserialize<List<CategoryView>>(subCategories);

                category = new CategoryView()
                {
                    CategoryName = categoryName,
                    SubCategories = subCategoriesList
                };

                if (ModelState.IsValid)
                {
                    try
                    {
                        pcs.AddCategory(category.CategoryName, category.SubCategories.ToList());
                        return Json(Url.Action("Index", "ProductCategoryManagement"));
                    }
                    catch (FaultException ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(category);
        }

        [HttpGet]
        public PartialViewResult CreateSubCategory()
        {
            return PartialView("_CreateSubcategory", new CategoryView());
        }

        // GET: ProductCategoryManagement/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryView category = new CategoryView();
            try
            {
                category = pcs.GetCategory(id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(category);
        }

        // POST: ProductCategoryManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Guid id, string categoryName, string subCategories)
        {
            CategoryView category = null;
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<CategoryView> subCategoriesList = js.Deserialize<List<CategoryView>>(subCategories);

                category = new CategoryView()
                {
                    CategoryId = id,
                    CategoryName = categoryName,
                    SubCategories = subCategoriesList
                };

                if (ModelState.IsValid)
                {
                    try
                    {
                        pcs.UpdateCategory(category.CategoryId, category.CategoryName, category.SubCategories.ToList());
                        return Json(Url.Action("Index", "ProductCategoryManagement"));
                    }
                    catch (FaultException ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(category);
        }

        // GET: ProductCategoryManagement/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryView category = new CategoryView();
            try
            {
                category = pcs.GetCategory(id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(category);
        }

        // POST: ProductCategoryManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                pcs.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(pcs.GetCategory(id));
        }
    }
}
