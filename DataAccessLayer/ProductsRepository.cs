using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Views;

namespace DataAccessLayer
{
    public class ProductsRepository : ConnectionClass
    {
        public ProductsRepository() : base() { }

        #region Products

        public IQueryable<ProductListView> GetLatestProducts(int count)
        {
            return GetAllProducts().OrderByDescending(p => p.DateAdded)
                .Select(p =>
                    new ProductListView()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImagePath = p.Image,
                        Price = p.Price
                    }).Take(count);
        }

        public IQueryable<Product> GetAllProducts()
        {
            return Entity.Products.Where(p => p.QtyAvailable > 0).Where(p => p.IsDeleted == false);
        }

        //date descending
        public IQueryable<ProductListView> GetAllProductsView()
        {
            return GetAllProducts().OrderByDescending(p => p.DateAdded).Select(p =>
                new ProductListView()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImagePath = p.Image,
                    Price = p.Price
                });
        }

        public IEnumerable<ProductListView> GetProductsByCategory(Guid categoryId)
        {
            return (from p in Entity.Products
                    join c in Entity.ProductCategories on p.ProductCategoryId equals c.Id
                    where (p.ProductCategoryId == categoryId || c.ParentId == categoryId) &&
                    p.QtyAvailable > 0 && p.IsDeleted == false
                    select new ProductListView()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImagePath = p.Image,
                    Price = p.Price
                });
        }

        public Product GetProduct(int productId)
        {
            return Entity.Products.SingleOrDefault(p => p.Id == productId);
        }

        public ShoppingCart GetShoppingCart(string userId, int prodId)
        {
            return Entity.ShoppingCarts.SingleOrDefault(sc => sc.UserId == userId && sc.ProductId == prodId);
        }

        public bool IsQuantityAvailable(int qtyRequested, int prodId)
        {
            return GetProduct(prodId).QtyAvailable >= qtyRequested;
        }

        public IEnumerable<CartItemView> GetShoppingCartItems(string userId)
        {
            return Entity.ShoppingCarts.Where(sc => sc.UserId == userId)
                .Select(sc => new CartItemView()
                {
                    ProductId = sc.ProductId,
                    ProductName = sc.Product.Name,
                    ProductPrice = sc.Product.Price,
                    ProductImagePath = sc.Product.Image,
                    ProductQty = sc.ProductQty,
                    TotalPrice = Math.Round(sc.ProductQty * sc.Product.Price, 2)
                });
        }

        public void AddProductToCart(ShoppingCart sc)
        {
            Entity.ShoppingCarts.Add(sc);
            Entity.SaveChanges();
        }

        public void UpdateCartItemQty(ShoppingCart sc)
        {
            ShoppingCart cartItem = GetShoppingCart(sc.UserId, sc.ProductId);
            cartItem.ProductQty = sc.ProductQty;
            Entity.SaveChanges();
        }

        public void DeleteCartItem(string userId, int prodId)
        {
            Entity.ShoppingCarts.Remove(GetShoppingCart(userId, prodId));
            Entity.SaveChanges();
        }

        public void ReduceStock(int prodId, int qtyPurchased)
        {
            Product p = GetProduct(prodId);
            p.QtyAvailable -= qtyPurchased;
            Entity.SaveChanges();
        }

        public int GetProductQuantity(int prodId)
        {
            return GetProduct(prodId).QtyAvailable;
        }

        public ProductDetailsView GetProductDetails(int prodId)
        {
            return GetAllProducts().Select(p => new ProductDetailsView()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ImagePath = p.Image,
                QtyAvailable = p.QtyAvailable,
                Price = p.Price
            }).SingleOrDefault(p => p.Id == prodId);
        }

        public IEnumerable<ProductView> GetProductsBySeller(Seller s)
        {
            return s.Products.Where(p => p.IsDeleted == false).Select(p => new ProductView()
            {
                ProductId = p.Id,
                Name = p.Name,
                Description = p.Description,
                Category = p.ProductCategory.Name,
                DateAdded = p.DateAdded,
                QtyAvailable = p.QtyAvailable,
                Price = p.Price,
                ImagePath = p.Image,
                CommissionType = p.ProductCommission.CommissionType.CommissionType1,
                CommissionAmount = p.ProductCommission.Amount
            });
        }

        public ProductView GetProductView(int id)
        {
            return Entity.Products.Select(p => new ProductView()
            {
                ProductId = p.Id,
                Name = p.Name,
                Description = p.Name,
                DateAdded = p.DateAdded,
                QtyAvailable = p.QtyAvailable,
                Price = p.Price,
                ImagePath = p.Image,
                CommissionTypeId = p.ProductCommission.CommissionTypeId,
                CommissionType = p.ProductCommission.CommissionType.CommissionType1,
                CommissionAmount = p.ProductCommission.Amount
            }).SingleOrDefault(p => p.ProductId == id);
        }

        public void AddProduct(Product p)
        {
            Entity.Products.Add(p);
            Entity.SaveChanges();
        }

        public void UpdateProduct(Product p)
        {
            Product product = GetProduct(p.Id);
            product.Name = p.Name;
            product.Description = p.Description;
            product.ProductCategoryId = p.ProductCategoryId;
            product.QtyAvailable = p.QtyAvailable;
            product.Price = p.Price;
            product.Image = p.Image;
            Entity.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            Product product = GetProduct(id);
            product.IsDeleted = true;
            Entity.SaveChanges();
        }

        public ProductCategory GetProductCategory(int id)
        {
            return GetProduct(id).ProductCategory;
        }

        #endregion

        #region Shopping Carts

        public int GetCartTotalNumberOfItems(string userId)
        {
            return Entity.ShoppingCarts.Where(s => s.UserId == userId).Count() == 0 ? 0
                : Entity.ShoppingCarts.Where(s => s.UserId == userId).Sum(s => s.ProductQty);
        }

        public decimal GetShoppingCartTotal(string userId)
        {
            if (Entity.ShoppingCarts.Where(s => s.UserId == userId).Count() == 0)
                return 0;
            else
            {
                var list = GetShoppingCartItems(userId);
                decimal total = 0;
                foreach (CartItemView sc in list)
                {
                    total += sc.ProductQty * sc.ProductPrice;
                }
                return Math.Round(total, 2);
            }
        }

        #endregion

        #region Product commissions

        public IEnumerable<CommissionTypeView> GetCommissionTypes()
        {
            return Entity.CommissionTypes.Select(ct => new CommissionTypeView()
            {
                CommissionTypeId = ct.Id,
                CommissionType = ct.CommissionType1
            });
        }

        public void UpdateProductCommission(ProductCommission pc)
        {
            ProductCommission commission = Entity.ProductCommissions.SingleOrDefault(c => c.ProductId == pc.ProductId);
            commission.CommissionTypeId = pc.CommissionTypeId;
            commission.Amount = pc.Amount;
            Entity.SaveChanges();
        }

        #endregion
    }
}
