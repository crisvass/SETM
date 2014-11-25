using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TradersMarketplace.Models;
using System.ServiceModel;
using System.Collections.Generic;
using System.Web.Security;

namespace TradersMarketplace.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                ModelState.Clear();
                if (new UsersServiceClient.UsersServiceClient().IsUserAuthenticated(model.Username, model.Password))
                {
                    Membership.ValidateUser(model.Username, model.Password);
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl)
                        && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//")
                        && !returnUrl.StartsWith("/\\"))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel rm = new RegisterViewModel();
            rm.RegisterModels = new List<Object>();
            rm.RegisterModels.Add(new RegisterBuyerViewModel());
            rm.RegisterModels.Add(new RegisterSellerViewModel());
            return View(rm);
        }

        // GET: /Account/_RegisterBuyerPartial
        [AllowAnonymous]
        public ActionResult RegisterBuyer()
        {
            return PartialView(new RegisterBuyerViewModel());
        }

        // POST: /Account/_RegisterBuyerPartial
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterBuyer(RegisterBuyerViewModel model)
        {
            try
            {
                new UsersServiceClient.UsersServiceClient().RegisterBuyer(model.Username,
                    model.Password, model.Email, model.Name, model.Surname, model.Residence,
                    model.Street, model.Town, model.PostCode, model.Country, model.CreditCardType,
                    model.CardHolderName, model.Month, model.Year);
                return RedirectToAction("LogOn", "Account");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            //if an error occurs
            return PartialView(model);
        }

        // GET: /Account/_RegisterSellerPartial
        [AllowAnonymous]
        public ActionResult RegisterSeller()
        {
            return PartialView(new RegisterSellerViewModel());
        }

        // POST: /Account/_RegisterSellerPartial
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterSeller(RegisterSellerViewModel model)
        {
            try
            {
                new UsersServiceClient.UsersServiceClient().RegisterSeller(model.Username,
                    model.Password, model.Email, model.Name, model.Surname, model.Residence,
                    model.Street, model.Town, model.PostCode, model.Country, model.RequiresDelivery,
                    model.IbanNumber);
                return RedirectToAction("LogOn", "Account");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            //if an error occurs
            return PartialView(model);
        }

    }
}