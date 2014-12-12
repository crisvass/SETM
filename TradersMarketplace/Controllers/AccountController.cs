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
using System.Net;
using Common.Models;

namespace TradersMarketplace.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToLocal(returnUrl);
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        //public ActionResult Login(LoginViewModel model, string returnUrl)
        //{
        //    try
        //    {
        //        ModelState.Clear();
        //        if (new UsersServiceClient.UsersServiceClient().IsUserAuthenticated(model.Username, model.Password))
        //        {
        //            Membership.ValidateUser(model.Username, model.Password);
        //            FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
        //            if (Url.IsLocalUrl(returnUrl)
        //                && returnUrl.Length > 1 && returnUrl.StartsWith("/")
        //                && !returnUrl.StartsWith("//")
        //                && !returnUrl.StartsWith("/\\"))
        //                return Redirect(returnUrl);
        //            else
        //                return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    catch (FaultException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //    }
        //    return View(model);
        //}

        // GET: /Account/RegisterBuyer
        [AllowAnonymous]
        public ActionResult RegisterBuyer()
        {
            return View(new RegisterBuyerViewModel());
        }

        // POST: /Account/RegisterBuyer
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterBuyer(RegisterBuyerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.BuyerUsername, Email = model.BuyerEmail };
                    var result = await UserManager.CreateAsync(user, model.BuyerPassword);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        return RedirectToAction("Index", "Home");
                    }

                }
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
        public async Task<ActionResult> RegisterSeller(RegisterSellerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.SellerUsername, Email = model.SellerPassword };
                    var result = await UserManager.CreateAsync(user, model.SellerPassword);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        return RedirectToAction("Index", "Home");
                    }

                }

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

        [Authorize()]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }


        #region Helpers
        // Used for XSRF protection when adding external logins
        //private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //internal class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri)
        //        : this(provider, redirectUri, null)
        //    {
        //    }

        //    public ChallengeResult(string provider, string redirectUri, string userId)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //        UserId = userId;
        //    }

            //public string LoginProvider { get; set; }
            //public string RedirectUri { get; set; }
            //public string UserId { get; set; }

            //public override void ExecuteResult(ControllerContext context)
            //{
            //    var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            //    if (UserId != null)
            //    {
            //        properties.Dictionary[XsrfKey] = UserId;
            //    }
            //    context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            //}
        //}
        #endregion

    }
}