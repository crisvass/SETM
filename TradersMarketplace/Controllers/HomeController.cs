using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Views;

namespace TradersMarketplace.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            List<ProductListView> products = new ProductsServiceClient.ProductsServiceClient().GetLatestProducts(9).ToList();
            return View("Index", products);
        }
    }
}