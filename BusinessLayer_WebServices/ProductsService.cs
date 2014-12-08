using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common.Views;
using DataAccessLayer;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductsService" in both code and config file together.
    public class ProductsService : IProductsService
    {
        public IQueryable<ProductListView> GetLatestProducts(int count)
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

        public IQueryable<ProductListView> GetAllProducts()
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
    }
}
