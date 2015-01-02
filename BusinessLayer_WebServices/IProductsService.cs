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
        IEnumerable<ProductListView> GetProductsByCategory(Guid categoryId);

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

        [OperationContract]
        IEnumerable<ProductView> GetProductsBySeller(string username);

        [OperationContract]
        ProductView GetProduct(int id);

        [OperationContract]
        void AddProduct(string name, string description, Guid categoryId, int qtyAvailable,
            decimal price, string imagePath, string sellerUsername, int commissionTypeId,
            decimal commissionAmount);

        [OperationContract]
        void UpdateProduct(int id, string name, string description, Guid categoryId, int qtyAvailable,
            decimal price, string imagePath, int commissionTypeId, decimal commissionAmount);

        [OperationContract]
        void DeleteProduct(int id);

        [OperationContract]
        IEnumerable<CommissionTypeView> GetCommissionTypes();
    }
}
