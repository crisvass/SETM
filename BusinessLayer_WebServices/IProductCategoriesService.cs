using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common.Views;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductCategoriesService" in both code and config file together.
    [ServiceContract]
    public interface IProductCategoriesService
    {
        [OperationContract]
        IEnumerable<CategoryView> GetCategories();

        [OperationContract]
        string GetCategoryName(Guid categoryId);

        [OperationContract]
        CategoryView GetCategory(Guid id);

        [OperationContract]
        void AddCategory(string categoryName, List<CategoryView> subCategories);

        [OperationContract]
        void UpdateCategory(Guid categoryId, string categoryName, List<CategoryView> subCategories);

        [OperationContract]
        void DeleteCategory(Guid id);

        [OperationContract]
        IEnumerable<CategoryView> GetMainCategories();

        [OperationContract]
        IEnumerable<CategoryView> GetSubcategories(Guid id);
    }
}
