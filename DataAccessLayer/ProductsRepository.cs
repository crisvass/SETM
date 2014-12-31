using System;
using System.Collections.Generic;
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
            return Entity.Products.Where(p => p.QtyAvailable > 0);
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
                    where p.ProductCategoryId == categoryId || c.ParentId == categoryId
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

        public ShoppingCart GetShoppingCart(string username, int prodId)
        {
            return Entity.ShoppingCarts.SingleOrDefault(sc => sc.Username == username && sc.ProductId == prodId);
        }

        public bool IsQuantityAvailable(int qtyRequested, int prodId)
        {
            return GetProduct(prodId).QtyAvailable >= qtyRequested;
        }

        public IEnumerable<CartItemView> GetShoppingCartItems(string username)
        {
            return Entity.ShoppingCarts.Where(sc => sc.Username == username)
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
            ShoppingCart cartItem = GetShoppingCart(sc.Username, sc.ProductId);
            cartItem.ProductQty = sc.ProductQty;
            Entity.SaveChanges();
        }

        public void DeleteCartItem(string username, int prodId)
        {
            Entity.ShoppingCarts.Remove(GetShoppingCart(username, prodId));
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
                //SellerUsername = p.SellerId
            }).SingleOrDefault(p => p.Id == prodId);
        }

        public int GetCartTotalNumberOfItems(string username)
        {
            return Entity.ShoppingCarts.Where(s => s.Username == username).Count() == 0 ? 0 
                : Entity.ShoppingCarts.Where(s => s.Username == username).Sum(s => s.ProductQty);
        }

        public decimal GetShoppingCartTotal(string username)
        {
            if(Entity.ShoppingCarts.Where(s => s.Username == username).Count() == 0)
                return 0;
            else{
                var list = GetShoppingCartItems(username);
                decimal total = 0;
                foreach (CartItemView sc in list)
                {
                    total += sc.ProductQty * sc.ProductPrice;
                }
                return Math.Round(total, 2);
            }                
        }
    }
}
