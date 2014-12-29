using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common.Views;

namespace BusinessLayer_WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductsService" in both code and config file together.
    [ServiceContract]
    public interface IProductsService
    {
        [OperationContract]
        IEnumerable<ProductListView> GetLatestProducts(int count);

        [OperationContract]
        IEnumerable<ProductListView> GetAllProducts();

        [OperationContract]
        IEnumerable<ProductListView> GetProductsByCategory(int categoryId);

        [OperationContract]
        bool IsCartQuantityRequestedAvailable(string username);

        [OperationContract]
        void AddProductToCart(string username, int productId, int qtyRequested);

        [OperationContract]
        void UpdateCart(List<CartItemView> cartItems, string username);

        [OperationContract]
        ShoppingCartView GetShoppingCart(string username);

        [OperationContract]
        ProductDetailsView GetProductDetails(int prodId);

        [OperationContract]
        int GetNumberOfItems(string username);
    }
}
