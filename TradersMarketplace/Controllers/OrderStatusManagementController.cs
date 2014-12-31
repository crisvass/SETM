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
    public class OrderStatusManagementController : BaseController
    {
        private OrdersServiceClient.OrdersServiceClient os = new OrdersServiceClient.OrdersServiceClient();

        // GET: OrderStatusManagement
        public ActionResult Index()
        {
            IEnumerable<OrderStatusView> statuses = null;
            try
            {
                statuses = os.GetOrderStatuses();
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(statuses);
        }

        // GET: OrderStatusManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderStatusView role = new OrderStatusView();
            try
            {
                role = os.GetOrderStatus((int)id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(role);
        }

        // GET: OrderStatusManagement/Create
        public ActionResult Create()
        {
            return View(new OrderStatusView());
        }

        // POST: OrderStatusManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderStatusView orderView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    os.AddOrderStatus(orderView.OrderStatus);
                    return RedirectToAction("Index");
                }
                catch (FaultException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(orderView);
        }

        // GET: OrderStatusManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderStatusView orderView = new OrderStatusView(); ;
            try
            {
                orderView = os.GetOrderStatus((int)id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(orderView);
        }

        // POST: OrderStatusManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderStatusView orderView)
        {
            try
            {
                os.UpdateOrderStatusItem(orderView.OrderStatusId, orderView.OrderStatus);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(orderView);
        }

        // GET: OrderStatusManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderStatusView orderView = null;
            try
            {
                orderView = os.GetOrderStatus((int)id);
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(orderView);
        }

        // POST: OrderStatusManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                os.DeleteOrderStatus(id);
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(os.GetOrderStatus(id));
        }
    }
}
