using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TradersMarketplace
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                string auth_param_name = "authid";
                string auth_cookie_name = "tradersMarketId";

                if (HttpContext.Current.Request[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }
            }
            catch
            {
            }
        }

        private void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            cookie_value = Request.Cookies["tradersMarketId"] == null ? string.Empty : Request.Cookies["tradersMarketId"].Value;
            if (null == cookie)
            {
                cookie = new HttpCookie(cookie_name);
            }

            if (Request.Cookies["tradersMarketId"] == null)
            {
                cookie.Value = string.Empty;
            }
            else
            {
                cookie.Value = Request.Cookies["tradersMarketId"].Value;
            }
            HttpContext.Current.Request.Cookies.Set(cookie);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Context.User != null)
            {
                try
                {
                    List<RolesServiceClient.RoleView> usersRoles = new RolesServiceClient.RolesServiceClient().GetUserRoles(Context.User.Identity.Name).ToList();
                    string[] roles = new string[usersRoles.Count()];
                    for (int i = 0; i < roles.Length; i++)
                    {
                        roles[i] = usersRoles.ElementAt(i).RoleName;
                    }
                    GenericPrincipal gp = new GenericPrincipal(Context.User.Identity, roles);
                    Context.User = gp;
                }
                catch (FaultException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
