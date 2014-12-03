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
    public class AccountController : BaseController
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

        [Authorize()]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    RegisterViewModel rm = new RegisterViewModel();
        //    rm.RegisterModels = new List<Object>();
        //    rm.RegisterModels.Add(new RegisterBuyerViewModel());
        //    rm.RegisterModels.Add(new RegisterSellerViewModel());
        //    return View(rm);
        //}

        // GET: /Account/RegisterBuyer
        [AllowAnonymous]
        public ActionResult RegisterBuyer()
        {
            return View(new RegisterBuyerViewModel());
            //return PartialView(new RegisterBuyerViewModel());
        }

        // POST: /Account/RegisterBuyer
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterBuyer(RegisterBuyerViewModel model)
        {
            try
            {
                new UsersServiceClient.UsersServiceClient().RegisterBuyer(model.BuyerUsername,
                    model.BuyerPassword, model.BuyerEmail, model.BuyerName, model.BuyerSurname, model.BuyerResidence,
                    model.BuyerStreet, model.BuyerTown, model.BuyerPostCode, model.BuyerCountry, model.BuyerCreditCardType,
                    model.BuyerCardHolderName, model.BuyerMonth, model.BuyerYear);
                return RedirectToAction("Login", "Account");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            //if an error occurs
            return View(model);
        }

        // GET: /Account/RegisterSeller
        [AllowAnonymous]
        public ActionResult RegisterSeller()
        {
            return View(new RegisterSellerViewModel());
        }

        // POST: /Account/RegisterSeller
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterSeller(RegisterSellerViewModel model)
        {
            try
            {
                new UsersServiceClient.UsersServiceClient().RegisterSeller(model.SellerUsername,
                    model.SellerPassword, model.SellerEmail, model.SellerName, model.SellerSurname, model.SellerResidence,
                    model.SellerStreet, model.SellerTown, model.SellerPostCode, model.SellerCountry, model.SellerRequiresDelivery,
                    model.SellerIbanNumber);
                return RedirectToAction("LogOn", "Account");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            //if an error occurs
            return View(model);
        }

    }
}