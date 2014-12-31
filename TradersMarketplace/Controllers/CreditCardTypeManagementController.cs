using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Models;
using System.ServiceModel;
using Common.Views;

namespace TradersMarketplace.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CreditCardTypeManagementController : BaseController
    {
        private CreditCardsServiceClient.CreditCardsServiceClient ccs = new CreditCardsServiceClient.CreditCardsServiceClient();

        // GET: CreditCardTypeManagement
        public ActionResult Index()
        {
            List<CreditCardTypeView> creditCardTypes = new List<CreditCardTypeView>();
            try
            {
                creditCardTypes = ccs.GetCreditCardTypes().ToList();
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(creditCardTypes);
        }

        // GET: CreditCardTypeManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCardTypeView creditCardType = new CreditCardTypeView();
            try
            {
                creditCardType = ccs.GetCreditCardType((int)id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(creditCardType);
        }

        // GET: CreditCardTypeManagement/Create
        public ActionResult Create()
        {
            return View(new CreditCardTypeView());
        }

        // POST: CreditCardTypeManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreditCardTypeView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ccs.AddCreditCardType(model.CreditCardType);
                    return RedirectToAction("Index");
                }
                catch (FaultException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        // GET: CreditCardTypeManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CreditCardTypeView creditCardType = new CreditCardTypeView(); ;
            try
            {
                creditCardType = ccs.GetCreditCardType((int)id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(creditCardType);
        }

        // POST: CreditCardTypeManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreditCardTypeView model)
        {
            try
            {
                ccs.UpdateCreditCardType(model.CreditCardTypeId, model.CreditCardType);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        // GET: CreditCardTypeManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CreditCardTypeView creditCardType = null;
            try
            {
                creditCardType = ccs.GetCreditCardType((int)id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(creditCardType);
        }

        // POST: CreditCardTypeManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ccs.DeleteCreditCardType(id);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(ccs.GetCreditCardType(id));
        }
    }
}
