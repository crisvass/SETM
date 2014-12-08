using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Controllers;

namespace TradersMarketplace.Views
{
    public class ProductController : BaseController
    {
        public ActionResult List()
        {
            ViewBag.ProductPageTitle = "All Products";
            return View("List", new ProductsServiceClient.ProductsServiceClient().GetAllProducts().ToList());
        }
    }
}