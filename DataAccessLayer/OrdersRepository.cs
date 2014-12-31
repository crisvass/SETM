using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Views;

namespace DataAccessLayer
{
    public class OrdersRepository : ConnectionClass
    {
        #region Orders
        public void AddOrder(Order o)
        {
            Entity.Orders.Add(o);
            Entity.SaveChanges();
        }

        public void UpdateOrderStatus(Order o)
        {
            Order order = GetOrder(o.Id);
            order.OrderStatusId = o.OrderStatusId;
            Entity.SaveChanges();
        }

        public Order GetOrder(Guid id)
        {
            return Entity.Orders.SingleOrDefault(o => o.Id == id);
        }

        public int GetProcessingStatusId()
        {
            return Entity.OrderStatuses.SingleOrDefault(os => os.Status.ToLower() == "processing").StatusId;
        }

        public void AddOrderDetail(OrderDetail od)
        {
            Entity.OrderDetails.Add(od);
            Entity.SaveChanges();
        }

        public decimal GetVatRate(Guid orderId)
        {
            return GetOrder(orderId).VatRate;
        }

        public IEnumerable<OrderDetailView> GetOrderDetails(Guid orderId)
        {
            return (from od in Entity.OrderDetails
                    where od.OrderId == orderId
                    select new OrderDetailView()
                    {
                        ProductName = od.Product.Name,
                        ProductPrice = od.ProductPrice,
                        ProductQty = od.ProductQty,
                        TotalPrice = od.ProductPrice * od.ProductQty
                    });
        }

        public decimal GetOrderTotal(Guid orderId)
        {
            var list = GetOrderDetails(orderId);
            decimal total = 0;
            foreach (OrderDetailView odv in list)
            {
                total += odv.ProductQty * odv.ProductPrice;
            }
            return Math.Round(total, 2);
        }

        public IEnumerable<OrderView> GetOrders()
        {
            return Entity.Orders.Select(o => new OrderView()
            {
                OrderId = o.Id,
                OrderDate = o.Date,
                Username = o.Username,
                VatRate = (int)(o.VatRate * 100),
                StatusId = o.OrderStatusId,
                Status = o.OrderStatus.Status
            });
        }

        public OrderView GetOrderView(Guid id)
        {
            OrderView order = Entity.Orders.Select(o => new OrderView()
            {
                OrderId = o.Id,
                OrderDate = o.Date,
                Username = o.Username,
                VatRate = (int)(o.VatRate * 100),
                StatusId = o.OrderStatusId,
                Status = o.OrderStatus.Status
            }).SingleOrDefault(o => o.OrderId == id);

            order.OrderTotal = GetOrderTotal(order.OrderId);
            return order;
        }

        public void CancelOrder(Guid id)
        {
            UpdateOrderStatus(new Order() { Id = id, OrderStatusId = GetCancelledOrderStatusId() });
        }

        #endregion

        #region Order Status

        public IEnumerable<OrderStatusView> GetOrderStatuses()
        {
            return Entity.OrderStatuses.Where(os => os.IsDeleted == false)
                .Select(os => new OrderStatusView()
            {
                OrderStatusId = os.StatusId,
                OrderStatus = os.Status
            });
        }

        public OrderStatus GetOrderStatus(int id)
        {
            return Entity.OrderStatuses.SingleOrDefault(os => os.StatusId == id);
        }

        public OrderStatusView GetOrderStatusView(int id)
        {
            return Entity.OrderStatuses.Select(os => new OrderStatusView()
            {
                OrderStatusId = os.StatusId,
                OrderStatus = os.Status
            }).SingleOrDefault(os => os.OrderStatusId == id);
        }

        public void AddOrderStatus(OrderStatus os)
        {
            Entity.OrderStatuses.Add(os);
            Entity.SaveChanges();
        }

        public void UpdateOrderStatus(OrderStatus os)
        {
            OrderStatus orderStatus = GetOrderStatus(os.StatusId);
            orderStatus.Status = os.Status;
            Entity.SaveChanges();
        }

        public void DeleteOrderStatus(int id)
        {
            OrderStatus orderStatus = GetOrderStatus(id);
            orderStatus.IsDeleted = true;
            Entity.SaveChanges();
        }

        public int GetCancelledOrderStatusId()
        {
            return GetOrderStatuses().SingleOrDefault(o => o.OrderStatus.ToLower() == "cancelled").OrderStatusId;
        }

        #endregion
    }
}
