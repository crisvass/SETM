using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Controllers;
using Common.Views;
using System.ServiceModel;

namespace TradersMarketplace.Views
{
    public class ProductController : BaseController
    {
        private ProductsServiceClient.ProductsServiceClient ps = new ProductsServiceClient.ProductsServiceClient();
        public ActionResult List(int? categoryId)
        {
            if (categoryId == null)
            {
                ViewBag.Title = "All Products";
                return View("List", new ProductsServiceClient.ProductsServiceClient().GetAllProducts().ToList());
            }
            else
            {
                ViewBag.Title = new ProductCategoriesServiceClient
                    .ProductCategoriesServiceClient().GetCategoryName((int)categoryId);
                return View("List", new ProductsServiceClient.ProductsServiceClient()
                    .GetProductsByCategory((int)categoryId).ToList());
            }
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductDetailsView product = new ProductDetailsView();
            try
            {
                product = ps.GetProductDetails((int)id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(product);
        }

        [Authorize(Roles = "Buyer")]
        [HttpPost]
        public void Details(int id, int qty)
        {
            try
            {
                ps.AddProductToCart(HttpContext.User.Identity.Name, id, qty);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet]
        public PartialViewResult UpdateCartTotal()
        {
            try
            {
                return PartialView("_CartItems", ps.GetNumberOfItems(HttpContext.User.Identity.Name));
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet]
        public ActionResult ShoppingCart()
        {
            try
            {
                return View(ps.GetShoppingCartItems(HttpContext.User.Identity.Name));
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}