using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Common.Views;
using TradersMarketplace.Models;

namespace TradersMarketplace.Controllers
{
    [CustomHandleErrorAttribute]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.Menus = new MenusServiceClient.MenusServiceClient().GetUserMenus(HttpContext.User.Identity.Name);
                ViewBag.CartItems = new ProductsServiceClient.ProductsServiceClient()
                    .GetNumberOfItems(HttpContext.User.Identity.Name);
            }
            else
            {
                ViewBag.Menus = new MenusServiceClient.MenusServiceClient().GetGuestMenus();                
            }
            
            ViewBag.Categories = new ProductCategoriesServiceClient.ProductCategoriesServiceClient().GetCategories();

            base.OnActionExecuting(filterContext);
        }
    }
}