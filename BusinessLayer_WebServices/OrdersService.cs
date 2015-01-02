using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;
using Common.CustomExceptions;
using Common.Views;
using DataAccessLayer;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrdersService" in both code and config file together.
    public class OrdersService : IOrdersService
    {
        public Guid PlaceOrder(string username)
        {
            try
            {
                ProductsRepository pr = new ProductsRepository();
                OrdersRepository or = new OrdersRepository();
                SettingsRepository sr = new SettingsRepository();
                UsersRepository ur = new UsersRepository();
                pr.Entity = or.Entity = sr.Entity;
                string userId = ur.GetUser(username).Id;

                Guid id = Guid.NewGuid();
                try
                {
                    pr.Entity.Database.Connection.Open();
                    pr.Transaction = or.Transaction = sr.Transaction = pr.Entity.Database.BeginTransaction();
                    or.AddOrder(new Order()
                    {
                        Id = id,
                        UserId = userId,
                        Date = DateTime.Now,
                        OrderStatusId = or.GetProcessingStatusId(),
                        VatRate = sr.GetVatRate()
                    });

                    foreach (CartItemView sc in pr.GetShoppingCartItems(userId))
                    {
                        or.AddOrderDetail(new OrderDetail()
                        {
                            OrderId = id,
                            ProductId = sc.ProductId,
                            ProductPrice = sc.ProductPrice,
                            ProductQty = sc.ProductQty
                        });

                        pr.ReduceStock(sc.ProductId, sc.ProductQty);
                    }
                    IEnumerable<CartItemView> items = pr.GetShoppingCartItems(userId);
                    foreach (CartItemView cv in items)
                    {
                        pr.DeleteCartItem(userId, cv.ProductId);
                    }

                    pr.Transaction.Commit();
                    return id;
                }
                catch
                {
                    pr.Transaction.Rollback();
                    throw new TransactionFailedException("Order could not be placed. Please try again or contact administrator if error persists.");
                }
                finally
                {
                    pr.Entity.Database.Connection.Close();
                }
            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("Error! Order could not be placed. Please contact administrator if error persists.");
            }
        }

        public void UpdateOrderStatus(Guid id, int statusId)
        {
            try
            {
                OrdersRepository or = new OrdersRepository();
                or.UpdateOrderStatus(new Order() { Id = id, OrderStatusId = statusId });
            }
            catch
            {
                throw new FaultException("Error! the order status could not be updated. Please contact administrator if error persists.");
            }
        }

        public InvoiceView GetInvoice(string username, Guid orderId)
        {
            try
            {
                OrdersRepository or = new OrdersRepository();
                UsersRepository ur = new UsersRepository();
                or.Entity = ur.Entity;

                decimal vatRate = Math.Round(or.GetVatRate(orderId), 2);
                decimal subtotal = or.GetOrderTotal(orderId);
                decimal vatAmount = Math.Round(vatRate * subtotal, 2);
                return new InvoiceView()
                {
                    OrderId = orderId,
                    OrderDate = or.GetOrder(orderId).Date,
                    Uv = ur.GetUserViewByUsername(username),
                    Items = or.GetOrderDetails(orderId).ToList(),
                    VatRate = (int)(vatRate * 100),
                    Subtotal = subtotal,
                    VatAmount = vatAmount,
                    Total = subtotal + vatAmount
                };
            }
            catch
            {
                throw new FaultException("Error! the order status could not be updated. Please contact administrator if error persists.");
            }
        }


        public IEnumerable<OrderStatusView> GetOrderStatuses()
        {
            try
            {
                return new OrdersRepository().GetOrderStatuses();
            }
            catch
            {
                throw new FaultException("Error whilst retrieving the list of order statuses. Please contact administrator if error persists.");
            }
        }

        public OrderStatusView GetOrderStatus(int id)
        {
            try
            {
                return new OrdersRepository().GetOrderStatusView(id);
            }
            catch
            {
                throw new FaultException("Error whilst retrieving the order status details. Please contact administrator if error persists.");
            }
        }

        public void AddOrderStatus(string status)
        {
            try
            {
                new OrdersRepository().AddOrderStatus(new OrderStatus() { Status = status });
            }
            catch
            {
                throw new FaultException("Error whilst adding the new order status. Please contact administrator if error persists.");
            }
        }

        public void UpdateOrderStatus(int id, string status)
        {
            try
            {
                new OrdersRepository().UpdateOrderStatus(new OrderStatus() { StatusId = id, Status = status });
            }
            catch
            {
                throw new FaultException("Error whilst updating the order status. Please contact administrator if error persists.");
            }
        }

        public void DeleteOrderStatus(int id)
        {
            try
            {
                new OrdersRepository().DeleteOrderStatus(id);
            }
            catch
            {
                throw new FaultException("Error whilst deleting the order status. Please contact administrator if error persists.");
            }
        }


        public IEnumerable<OrderView> GetOrders()
        {
            try
            {
                OrdersRepository or = new OrdersRepository();
                IEnumerable<OrderView> orders = or.GetOrders();
                List<OrderView> ordersComplete = new List<OrderView>();
                OrderView order = null;
                foreach (OrderView ov in orders)
                {
                    order = ov;
                    order.OrderTotal = or.GetOrderTotal(order.OrderId);
                    ordersComplete.Add(order);
                }
                return ordersComplete.AsEnumerable<OrderView>();
            }
            catch
            {
                throw new FaultException("Error whilst retrieving orders. Please contact administrator if error persists.");
            }
        }

        public OrderView GetOrder(Guid id)
        {
            try
            {
                OrdersRepository or  = new OrdersRepository();
                OrderView order = or.GetOrderView(id);
                order.OrderTotal = or.GetOrderTotal(order.OrderId);
                order.OrderItems = or.GetOrderDetails(order.OrderId);
                return order;
            }
            catch
            {
                throw new FaultException("Error whilst retrieving order details. Please contact administrator if error persists.");
            }
        }

        public void CancelOrder(Guid id)
        {
            try
            {
                new OrdersRepository().CancelOrder(id);
            }
            catch
            {
                throw new FaultException("Error whilst deleting the order status. Please contact administrator if error persists.");
            }
        }
    }
}
