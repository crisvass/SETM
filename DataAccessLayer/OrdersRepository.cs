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
    }
}
