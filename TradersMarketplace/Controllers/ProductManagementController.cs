using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Common.Views;
using TradersMarketplace.Models;
using TradersMarketplace.Utilities;

namespace TradersMarketplace.Controllers
{
    [Authorize(Roles = "Seller")]
    public class ProductManagementController : BaseController
    {
        private ProductsServiceClient.ProductsServiceClient ps = new ProductsServiceClient.ProductsServiceClient();
        private ProductCategoriesServiceClient.ProductCategoriesServiceClient pcs =
            new ProductCategoriesServiceClient.ProductCategoriesServiceClient();

        // GET: ProductManagement
        public ActionResult Index()
        {
            List<ProductView> products = null;
            try
            {
                products = ps.GetProductsBySeller(HttpContext.User.Identity.Name);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(products);
        }

        // GET: ProductManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductView product = new ProductView();
            try
            {
                product = ps.GetProduct((int)id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(product);
        }

        // GET: ProductManagement/Create
        public ActionResult Create()
        {
            ProductView product = null;
            try
            {
                product = new ProductView()
                {
                    CategoriesList = new SelectList(pcs.GetMainCategories(), "CategoryId", "CategoryName"),
                    SubcategoriesList = new SelectList(new List<CategoryView>(), "CategoryId", "CategoryName"),
                    CommissionTypesList = new SelectList(ps.GetCommissionTypes(), "CommissionTypeId", "CommissionType")
                };
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(product);
        }

        [HttpPost]
        public PartialViewResult GetProductSubcategories(Guid parentId)
        {
            return PartialView("_SubcategoriesSelectList", pcs.GetSubcategories(parentId));
        }

        // POST: ProductManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductView product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ps.AddProduct(product.Name, product.Description, product.CategoryId, product.QtyAvailable,
                        product.Price, ImageUpload.SaveImage(product.ImagePath), HttpContext.User.Identity.Name,
                        product.CommissionTypeId, product.CommissionAmount);
                    return RedirectToAction("Index");
                }
                catch (FaultException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(product);
        }

        // GET: ProductManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductView product = new ProductView(); ;
            try
            {
                product = ps.GetProduct((int)id);
                product.CategoriesList = new SelectList(pcs.GetMainCategories(), "CategoryId", "CategoryName");
                product.SubcategoriesList = new SelectList(pcs.GetSubcategories(product.CategoryId),
                    "CategoryId", "CategoryName", product.SubcategoryId);
                product.CommissionTypesList = new SelectList(ps.GetCommissionTypes(), "CommissionTypeId", "CommissionType");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(product);
        }

        // POST: ProductManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductView product)
        {
            try
            {
                string imagePath = product.ImagePath;
                if (ImageUpload.IsBase64String(product.ImagePath))
                {
                    imagePath = ImageUpload.SaveImage(product.ImagePath);
                }
                ps.UpdateProduct(product.ProductId, product.Name, product.Description, product.CategoryId, product.QtyAvailable,
                        product.Price, imagePath, product.CommissionTypeId,
                        product.CommissionAmount);

                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(product);
        }

        // GET: ProductManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductView product = null;
            try
            {
                product = ps.GetProduct((int)id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(product);
        }

        // POST: ProductManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ps.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(ps.GetProduct(id));
        }
    }
}
