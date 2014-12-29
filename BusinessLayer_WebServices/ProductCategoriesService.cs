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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductCategoriesService" in both code and config file together.
    public class ProductCategoriesService : IProductCategoriesService
    {
        public IEnumerable<CategoryView> GetCategories()
        {
            try
            {
                return new CategoriesRepository().GetCategories();
            }
            catch
            {
                throw new FaultException("Error whilst retrieving categories. Please contact administrator if error persists.");
            }
        }

        public string GetCategoryName(int categoryId)
        {
            try
            {
                return new CategoriesRepository().GetCategoryName(categoryId);
            }
            catch
            {
                throw new FaultException("Error whilst retrieving categories. Please contact administrator if error persists.");
            }
        }
    }
}
