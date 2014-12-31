using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Models;
using Common.Views;
using System.ServiceModel;

namespace TradersMarketplace.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrderManagementController : BaseController
    {
        private OrdersServiceClient.OrdersServiceClient os = new OrdersServiceClient.OrdersServiceClient();

        // GET: OrderManagement
        public ActionResult Index()
        {
            IEnumerable<OrderView> orders = null;
            try
            {
                orders = os.GetOrders();
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(orders);
        }

        // GET: OrderManagement/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderView order = new OrderView();
            try
            {
                order = os.GetOrder(id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(order);
        }        

        // GET: OrderManagement/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderView order = new OrderView();
            try
            {
                order = os.GetOrder(id);
                order.StatusList = new SelectList(os.GetOrderStatuses(), "OrderStatusId", "OrderStatus", order.StatusId);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(order);
        }

        // POST: OrderManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderView order)
        {
            try
            {
                os.UpdateOrderStatus(order.OrderId, order.StatusId);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(order);
        }

        // GET: OrderManagement/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderView order = null;
            try
            {
                order = os.GetOrder(id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(order);
        }

        // POST: OrderManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                os.CancelOrder(id);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(os.GetOrder(id));
        }
    }
}
