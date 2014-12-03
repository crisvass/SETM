using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Views;

namespace TradersMarketplace.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.Menus = new MenusServiceClient.MenusServiceClient().GetUserMenus(HttpContext.User.Identity.Name);
                //ViewBag.CartItems = new ShoppingCartsServiceClient().GetNumberOfItems(HttpContext.User.Identity.Name);
            }
            else
            {
                ViewBag.Menus = new MenusServiceClient.MenusServiceClient().GetGuestMenus();
                //ViewBag.CartItems = 0;
            }

            //foreach (MenusView mv in ViewBag.Menus)
            //{
            //    if (mv.HasSubmenus)
            //    {
            //        string title = "Submenus" + (mv.Title);
            //        ViewData.Add(title, new MenusServiceClient.MenusServiceClient().GetSubMenus(mv.MenuId));
            //    }
            //}
            
            base.OnActionExecuting(filterContext);
        }
    }
}