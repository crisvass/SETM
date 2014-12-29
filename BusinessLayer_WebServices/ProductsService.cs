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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductsService" in both code and config file together.
    public class ProductsService : IProductsService
    {
        public IEnumerable<ProductListView> GetLatestProducts(int count)
        {
            try
            {
                return new ProductsRepository().GetLatestProducts(count);
            }
            catch
            {
                throw new FaultException("Error whilst retrieving latest products. Please contact administrator if error persists.");
            }
        }

        public IEnumerable<ProductListView> GetAllProducts()
        {
            try
            {
                return new ProductsRepository().GetAllProductsView();
            }
            catch
            {
                throw new FaultException("Error whilst retrieving products. Please contact administrator if error persists.");
            }
        }

        public IEnumerable<ProductListView> GetProductsByCategory(int categoryId)
        {
            try
            {
                return new ProductsRepository().GetProductsByCategory(categoryId);
            }
            catch
            {
                throw new FaultException("Error whilst retrieving products. Please contact administrator if error persists.");
            }
        }

        public bool IsCartQuantityRequestedAvailable(string username)
        {
            try
            {
                ProductsRepository pr = new ProductsRepository();
                bool qtyAvailable = true;
                IEnumerable<CartItemView> cartItems = pr.GetShoppingCartItems(username);
                foreach (CartItemView cv in cartItems)
                {
                    if (!pr.IsQuantityAvailable(cv.ProductQty, cv.ProductId))
                    {
                        qtyAvailable = false;
                        pr.DeleteCartItem(username, cv.ProductId);
                    }
                }
                return qtyAvailable;
            }
            catch
            {
                throw new FaultException("Error whilst checking shopping cart quantities availability. Please contact administrator if error persists.");
            }
        }

        public void AddProductToCart(string username, int productId, int qtyRequested)
        {
            if (qtyRequested > 0)
            {
                try
                {
                    ProductsRepository pr = new ProductsRepository();

                    if (pr.GetShoppingCart(username, productId) == null)
                    {
                        if (pr.IsQuantityAvailable(qtyRequested, productId))
                            pr.AddProductToCart(new ShoppingCart() { Username = username, ProductId = productId, ProductQty = qtyRequested });
                        else
                            throw new ProductQuantityNotAvailable();
                    }
                    else
                    {
                        pr.UpdateCartItemQty(new ShoppingCart() { Username = username, ProductId = productId, ProductQty = qtyRequested });
                    }
                }
                catch (ProductQuantityNotAvailable ex)
                {
                    throw new FaultException(ex.Message);
                }
                catch
                {
                    throw new FaultException("Error whilst checking shopping cart quantities availability. Please contact administrator if error persists.");
                }
            }
        }

        public void UpdateCart(List<CartItemView> cartItems, string username)
        {
            bool qtyAvailable = true;
            try
            {
                ProductsRepository pr = new ProductsRepository();
                ShoppingCart sc = new ShoppingCart();
                try
                {
                    pr.Entity.Database.Connection.Open();
                    pr.Transaction = pr.Entity.Database.BeginTransaction();

                    foreach (CartItemView civ in cartItems)
                    {
                        sc = pr.GetShoppingCart(username, civ.ProductId);
                        if (pr.IsQuantityAvailable(civ.ProductQty, civ.ProductId))
                        {
                            if (sc.ProductQty == 0)
                                pr.DeleteCartItem(username, civ.ProductId);
                            else
                            {
                                sc.ProductQty = civ.ProductQty;
                                pr.UpdateCartItemQty(sc);
                            }
                        }
                        else
                        {
                            sc.ProductQty = pr.GetProductQuantity(civ.ProductId);
                            pr.UpdateCartItemQty(sc);
                            qtyAvailable = false;
                        }
                    }
                    pr.Transaction.Commit();


                }
                catch
                {
                    pr.Transaction.Rollback();
                    throw new TransactionFailedException("Update shopping cart Failed. Pelase try again later or contact administrator.");
                }
                finally
                {
                    pr.Entity.Database.Connection.Close();
                }

                if (!qtyAvailable)
                    throw new ProductQuantityNotAvailable("The quantity requested was not available for all products.");
            }
            catch (ProductQuantityNotAvailable ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (TransactionFailedException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch
            {
                throw new FaultException("Shopping Cart could not be updated. Please try again or contact administrator if error persists.");
            }
        }

        public ShoppingCartView GetShoppingCart(string username)
        {
            try
            {
                ProductsRepository pr = new ProductsRepository();
                SettingsRepository sr = new SettingsRepository();
                pr.Entity = sr.Entity;

                decimal vatRate = Math.Round(sr.GetVatRate() * 100, 2);
                decimal subtotal = pr.GetShoppingCartTotal(username);
                decimal vatAmount = Math.Round(vatRate * subtotal,2);
                return new ShoppingCartView()
                {
                    CartItems = pr.GetShoppingCartItems(username).ToList(),
                    VatRate = (int)vatRate,
                    Subtotal = subtotal,
                    VatAmount = vatAmount,
                    Total = subtotal + vatAmount
                };
            }
            catch
            {
                throw new FaultException("Error whilst retrieving your shopping cart items. Please contact administrator if error persists.");
            }
        }

        public ProductDetailsView GetProductDetails(int prodId)
        {
            return new ProductsRepository().GetProductDetails(prodId);
        }

        public int GetNumberOfItems(string username)
        {
            try
            {
                return new ProductsRepository().GetCartTotalNumberOfItems(username);
            }
            catch
            {
                throw new FaultException("Error whilst retrieving number of items in shopping cart. Please contact administrator if error persists.");
            }
        }
    }
}
