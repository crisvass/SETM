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
    }
}
