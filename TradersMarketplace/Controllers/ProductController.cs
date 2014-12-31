using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Controllers;
using Common.Views;
using System.ServiceModel;
using System.Web.Script.Serialization;
using TradersMarketplace.Models;
using System.Globalization;

namespace TradersMarketplace.Views
{
    public class ProductController : BaseController
    {
        private ProductsServiceClient.ProductsServiceClient ps = new ProductsServiceClient.ProductsServiceClient();
        private UsersServiceClient.UsersServiceClient us = new UsersServiceClient.UsersServiceClient();
        private CreditCardsServiceClient.CreditCardsServiceClient ccs = new CreditCardsServiceClient.CreditCardsServiceClient();
        private OrdersServiceClient.OrdersServiceClient os = new OrdersServiceClient.OrdersServiceClient();

        public ActionResult List(Guid categoryId)
        {
            if (categoryId == null)
            {
                ViewBag.Title = "All Products";
                return View("List", new ProductsServiceClient.ProductsServiceClient().GetAllProducts().ToList());
            }
            else
            {
                ViewBag.Title = new ProductCategoriesServiceClient
                    .ProductCategoriesServiceClient().GetCategoryName(categoryId);
                return View("List", new ProductsServiceClient.ProductsServiceClient()
                    .GetProductsByCategory(categoryId).ToList());
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
                return View(ps.GetShoppingCart(HttpContext.User.Identity.Name));
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
        [HttpPost]
        public void ShoppingCart(string carts)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<CartItemView> scs = js.Deserialize<List<CartItemView>>(carts);

                ps.UpdateCart(scs, HttpContext.User.Identity.Name);
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
        public PartialViewResult GetShoppingCartList()
        {
            ShoppingCartView cart = ps.GetShoppingCart(HttpContext.User.Identity.Name);
            return PartialView("_ShoppingCartItems", cart);
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet]
        public PartialViewResult GetShoppingCartTotals()
        {
            ShoppingCartView cart = ps.GetShoppingCart(HttpContext.User.Identity.Name);
            return PartialView("_ShoppingCartTotals", cart);
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet]
        public ActionResult Checkout()
        {
            CheckoutView view = new CheckoutView();
            view.Uv = new UserView();
            view.Ccdv = new CreditCardDetailView();

            try
            {
                view.Ccdv.CreditCardTypeList = new SelectList(ccs.GetCreditCardTypes(),
                    "CreditCardTypeId", "CreditCardType");

                view.Uv = us.GetUserForCheckout(HttpContext.User.Identity.Name);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            List<MonthModel> months = new List<MonthModel>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(new MonthModel() { MonthNum = i, Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i) });
            }
            string[] years = new string[5];

            for (int i = 0; i < years.Length; i++)
            {
                years[i] = (DateTime.Now.Year + i).ToString();
            }

            view.Ccdv.MonthsList = new SelectList(months, "MonthNum", "Month");
            view.Ccdv.YearsList = new SelectList(years);

            return View(view);
        }

        [Authorize(Roles = "Buyer")]
        [HttpPost]
        public ActionResult Checkout(string email, string contactNumber, string residence,
            string street, string postCode, string town, string country, string creditCards)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<CreditCardDetailView> creditCardsList = js.Deserialize<List<CreditCardDetailView>>(creditCards);

                string username = HttpContext.User.Identity.Name;
                UserView user = new UserView()
                {
                    Username = username,
                    Email = email,
                    ContactNumber = contactNumber,
                    Residence = residence,
                    Street = street,
                    Town = town,
                    PostCode = postCode,
                    Country = country,
                    CreditCards = creditCardsList
                };

                if (ModelState.IsValid)
                {
                    try
                    {
                        us.UpdateUserPartial(username, email, residence, street, town, postCode,
                             country, contactNumber, creditCardsList);

                        Guid orderId = os.PlaceOrder(username);

                        return Json(Url.Action("Invoice", "Product", new { orderId = orderId }));
                    }
                    catch (FaultException ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(Url.Action("Checkout", "Product"));
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet]
        public ActionResult Invoice(Guid orderId)
        {
            InvoiceView iv = new InvoiceView();
            try
            {
                iv = os.GetInvoice(HttpContext.User.Identity.Name, orderId);
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(iv);
        }
    }
}