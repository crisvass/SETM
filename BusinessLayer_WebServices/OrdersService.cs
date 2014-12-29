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
        public void PlaceOrder(string username)
        {
            try
            {
                ProductsRepository pr = new ProductsRepository();
                OrdersRepository or = new OrdersRepository();
                SettingsRepository sr = new SettingsRepository();
                pr.Entity = or.Entity = sr.Entity;

                Guid id = Guid.NewGuid();
                try
                {
                    pr.Entity.Database.Connection.Open();
                    pr.Transaction = or.Transaction = sr.Transaction = pr.Entity.Database.BeginTransaction();
                    or.AddOrder(new Order() { Id = id, Username = username, Date = DateTime.Now, 
                        OrderStatusId = or.GetProcessingStatusId(), VatRate = sr.GetVatRate()});

                    foreach (CartItemView sc in pr.GetShoppingCartItems(username))
                    {
                        or.AddOrderDetail(new OrderDetail() { OrderId = id, ProductId = sc.ProductId,  
                            ProductPrice = sc.ProductPrice, ProductQty = sc.ProductQty});

                        pr.ReduceStock(sc.ProductId, sc.ProductQty);
                    }
                    IEnumerable<CartItemView> items = pr.GetShoppingCartItems(username);
                    foreach (CartItemView cv in items)
                    {
                        pr.DeleteCartItem(username, cv.ProductId);
                    }

                    pr.Transaction.Commit();
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
            throw new NotImplementedException();
        }
    }
}
