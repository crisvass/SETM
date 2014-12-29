using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccessLayer
{
    public class OrdersRepository:ConnectionClass
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

        public void AddOrderDetail(OrderDetail od){
            Entity.OrderDetails.Add(od);
            Entity.SaveChanges();
        }
    }
}
