using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BusinessLayer_WebServices.AdvancedSearchDecorator;
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

        public IEnumerable<ProductListView> GetProductsByCategory(Guid categoryId)
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
                UsersRepository ur = new UsersRepository();
                pr.Entity = ur.Entity;
                bool qtyAvailable = true;
                IEnumerable<CartItemView> cartItems = pr.GetShoppingCartItems(ur.GetUser(username).Id);
                foreach (CartItemView cv in cartItems)
                {
                    if (!pr.IsQuantityAvailable(cv.ProductQty, cv.ProductId))
                    {
                        qtyAvailable = false;
                        pr.DeleteCartItem(ur.GetUser(username).Id, cv.ProductId);
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
                    UsersRepository ur = new UsersRepository();
                    pr.Entity = ur.Entity;

                    if (pr.GetShoppingCart(ur.GetUser(username).Id, productId) == null)
                    {
                        if (pr.IsQuantityAvailable(qtyRequested, productId))
                            pr.AddProductToCart(new ShoppingCart() { UserId = ur.GetUser(username).Id, ProductId = productId, ProductQty = qtyRequested });
                        else
                            throw new ProductQuantityNotAvailable();
                    }
                    else
                    {
                        pr.UpdateCartItemQty(new ShoppingCart() { UserId = ur.GetUser(username).Id, ProductId = productId, ProductQty = qtyRequested });
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
                UsersRepository ur = new UsersRepository();
                pr.Entity = ur.Entity;
                try
                {
                    pr.Entity.Database.Connection.Open();
                    pr.Transaction = ur.Transaction = pr.Entity.Database.BeginTransaction();

                    foreach (CartItemView civ in cartItems)
                    {
                        sc = pr.GetShoppingCart(ur.GetUser(username).Id, civ.ProductId);
                        if (pr.IsQuantityAvailable(civ.ProductQty, civ.ProductId))
                        {
                            if (sc.ProductQty == 0)
                                pr.DeleteCartItem(ur.GetUser(username).Id, civ.ProductId);
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
                UsersRepository ur = new UsersRepository();
                pr.Entity = sr.Entity = ur.Entity;

                decimal vatRate = Math.Round(sr.GetVatRate(), 2);
                decimal subtotal = pr.GetShoppingCartTotal(ur.GetUser(username).Id);
                decimal vatAmount = Math.Round(vatRate * subtotal, 2);
                return new ShoppingCartView()
                {
                    CartItems = pr.GetShoppingCartItems(ur.GetUser(username).Id).ToList(),
                    VatRate = (int)(vatRate * 100),
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
                ProductsRepository pr = new ProductsRepository();
                UsersRepository ur = new UsersRepository();
                pr.Entity = ur.Entity;
                return pr.GetCartTotalNumberOfItems(ur.GetUser(username).Id);
            }
            catch
            {
                throw new FaultException("Error whilst retrieving number of items in shopping cart. Please contact administrator if error persists.");
            }
        }


        public IEnumerable<ProductView> GetProductsBySeller(string username)
        {
            try
            {
                ProductsRepository pr = new ProductsRepository();
                UsersRepository ur = new UsersRepository();
                pr.Entity = ur.Entity;
                return pr.GetProductsBySeller(ur.GetSeller(ur.GetUser(username).Id));
            }
            catch
            {
                throw new FaultException("Error whilst retrieving products. Please contact administrator if error persists.");
            }
        }

        public ProductView GetProduct(int id)
        {
            try
            {
                ProductsRepository pr = new ProductsRepository();
                CategoriesRepository cr = new CategoriesRepository();
                ProductView product = pr.GetProductView(id);
                ProductCategory category = pr.GetProductCategory(id);
                if (cr.IsSubcategory(category))
                {
                    ProductCategory pc = cr.GetParentCategory(category);
                    product.CategoryId = pc.Id;
                    product.Category = pc.Name;
                    product.SubcategoryId = category.Id;
                    product.Subcategory = category.Name;
                }
                else
                {
                    product.CategoryId = category.Id;
                    product.Category = category.Name;
                }
                return product;
            }
            catch
            {
                throw new FaultException("Error whilst retrieving product. Please contact administrator if error persists.");
            }
        }

        public void AddProduct(string name, string description, Guid categoryId, int qtyAvailable,
            decimal price, string imagePath, string sellerUsername, int commissionTypeId,
            decimal commissionAmount)
        {
            ProductsRepository pr = new ProductsRepository();
            UsersRepository ur = new UsersRepository();
            pr.Entity = ur.Entity;

            try
            {
                Product p = new Product()
                {
                    Name = name,
                    Description = description,
                    ProductCategoryId = categoryId,
                    QtyAvailable = qtyAvailable,
                    Price = price,
                    Image = imagePath,
                    SellerId = ur.GetUser(sellerUsername).Id,
                    ProductCommission = new ProductCommission()
                    {
                        CommissionTypeId = commissionTypeId,
                        Amount = commissionAmount
                    },
                    DateAdded = DateTime.Now
                };

                try
                {
                    pr.Entity.Database.Connection.Open();
                    pr.Transaction = ur.Transaction = ur.Entity.Database.BeginTransaction();
                    pr.AddProduct(p);

                    pr.Transaction.Commit();
                }
                catch
                {
                    pr.Transaction.Rollback();
                    throw new TransactionFailedException("Adding a new product failed. Please try again or contact administrator if error persists.");
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
                throw new FaultException("Error whilst adding a new product. Please try again or contact administrator if error persists.");
            }
        }

        public void UpdateProduct(int id, string name, string description, Guid categoryId, int qtyAvailable,
            decimal price, string imagePath, int commissionTypeId, decimal commissionAmount)
        {
            ProductsRepository pr = new ProductsRepository();
            UsersRepository ur = new UsersRepository();
            pr.Entity = ur.Entity;

            try
            {
                Product p = new Product()
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    ProductCategoryId = categoryId,
                    QtyAvailable = qtyAvailable,
                    Price = price,
                    Image = imagePath
                };

                ProductCommission pc = new ProductCommission()
                {
                    ProductId = id,
                    CommissionTypeId = commissionTypeId,
                    Amount = commissionAmount
                };

                try
                {
                    pr.Entity.Database.Connection.Open();
                    pr.Transaction = ur.Transaction = ur.Entity.Database.BeginTransaction();

                    pr.UpdateProduct(p);
                    pr.UpdateProductCommission(pc);

                    pr.Transaction.Commit();
                }
                catch
                {
                    pr.Transaction.Rollback();
                    throw new TransactionFailedException("Updating product failed. Please try again or contact administrator if error persists.");
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
                throw new FaultException("Error whilst updating the product. Please try again or contact administrator if error persists.");
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                new ProductsRepository().DeleteProduct(id);
            }
            catch
            {
                throw new FaultException("Error whilst deleting the product. Please try again or contact administrator if error persists.");
            }
        }


        public IEnumerable<CommissionTypeView> GetCommissionTypes()
        {
            try
            {
                return new ProductsRepository().GetCommissionTypes();
            }
            catch
            {
                throw new FaultException("Error whilst retrieving product commission types. Please try again or contact administrator if error persists.");
            }
        }


        public IEnumerable<ProductListView> AdvancedSearch(string nameKeyword, string descKeyword, Guid categoryId, Guid subcategoryId)
        {
            try
            {
                ProductsRepository pr = new ProductsRepository();
                IEnumerable<Product> products = pr.GetAllProducts();

                AdvancedSearchDecorator.AdvancedSearchDecorator search =
                    new ByNameKeywordSearch(
                        new ByDescriptionKeywordSearch(
                                new ByCategorySearch(
                                        new BySubcategorySearch() { SubcategoryId = subcategoryId }
                                    ) { CategoryId = categoryId }
                            ) { Keyword = descKeyword }
                        ) { Keyword = nameKeyword };

                return search.Search(products).Select(p => new ProductListView()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImagePath = p.Image,
                    Price = p.Price
                });
            }
            catch
            {
                throw new FaultException("Error whilst retrieving search results. Please try again or contact administrator if error persists.");
            }
        }
    }
}
