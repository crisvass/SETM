﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TradersMarketplace.ProductsServiceClient {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProductListView", Namespace="http://schemas.datacontract.org/2004/07/Common.Views")]
    [System.SerializableAttribute()]
    public partial class ProductListView : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ImagePathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PriceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ImagePath {
            get {
                return this.ImagePathField;
            }
            set {
                if ((object.ReferenceEquals(this.ImagePathField, value) != true)) {
                    this.ImagePathField = value;
                    this.RaisePropertyChanged("ImagePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CartItemView", Namespace="http://schemas.datacontract.org/2004/07/Common.Views")]
    [System.SerializableAttribute()]
    public partial class CartItemView : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ProductIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProductImagePathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProductNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal ProductPriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ProductQtyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal TotalPriceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ProductId {
            get {
                return this.ProductIdField;
            }
            set {
                if ((this.ProductIdField.Equals(value) != true)) {
                    this.ProductIdField = value;
                    this.RaisePropertyChanged("ProductId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductImagePath {
            get {
                return this.ProductImagePathField;
            }
            set {
                if ((object.ReferenceEquals(this.ProductImagePathField, value) != true)) {
                    this.ProductImagePathField = value;
                    this.RaisePropertyChanged("ProductImagePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductName {
            get {
                return this.ProductNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ProductNameField, value) != true)) {
                    this.ProductNameField = value;
                    this.RaisePropertyChanged("ProductName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ProductPrice {
            get {
                return this.ProductPriceField;
            }
            set {
                if ((this.ProductPriceField.Equals(value) != true)) {
                    this.ProductPriceField = value;
                    this.RaisePropertyChanged("ProductPrice");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ProductQty {
            get {
                return this.ProductQtyField;
            }
            set {
                if ((this.ProductQtyField.Equals(value) != true)) {
                    this.ProductQtyField = value;
                    this.RaisePropertyChanged("ProductQty");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal TotalPrice {
            get {
                return this.TotalPriceField;
            }
            set {
                if ((this.TotalPriceField.Equals(value) != true)) {
                    this.TotalPriceField = value;
                    this.RaisePropertyChanged("TotalPrice");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ShoppingCartView", Namespace="http://schemas.datacontract.org/2004/07/Common.Views")]
    [System.SerializableAttribute()]
    public partial class ShoppingCartView : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<Common.Views.CartItemView> CartItemsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal SubtotalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal TotalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal VatAmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int VatRateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<Common.Views.CartItemView> CartItems {
            get {
                return this.CartItemsField;
            }
            set {
                if ((object.ReferenceEquals(this.CartItemsField, value) != true)) {
                    this.CartItemsField = value;
                    this.RaisePropertyChanged("CartItems");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Subtotal {
            get {
                return this.SubtotalField;
            }
            set {
                if ((this.SubtotalField.Equals(value) != true)) {
                    this.SubtotalField = value;
                    this.RaisePropertyChanged("Subtotal");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Total {
            get {
                return this.TotalField;
            }
            set {
                if ((this.TotalField.Equals(value) != true)) {
                    this.TotalField = value;
                    this.RaisePropertyChanged("Total");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal VatAmount {
            get {
                return this.VatAmountField;
            }
            set {
                if ((this.VatAmountField.Equals(value) != true)) {
                    this.VatAmountField = value;
                    this.RaisePropertyChanged("VatAmount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int VatRate {
            get {
                return this.VatRateField;
            }
            set {
                if ((this.VatRateField.Equals(value) != true)) {
                    this.VatRateField = value;
                    this.RaisePropertyChanged("VatRate");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProductDetailsView", Namespace="http://schemas.datacontract.org/2004/07/Common.Views")]
    [System.SerializableAttribute()]
    public partial class ProductDetailsView : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ImagePathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PriceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int QtyAvailableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SellerUsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ImagePath {
            get {
                return this.ImagePathField;
            }
            set {
                if ((object.ReferenceEquals(this.ImagePathField, value) != true)) {
                    this.ImagePathField = value;
                    this.RaisePropertyChanged("ImagePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int QtyAvailable {
            get {
                return this.QtyAvailableField;
            }
            set {
                if ((this.QtyAvailableField.Equals(value) != true)) {
                    this.QtyAvailableField = value;
                    this.RaisePropertyChanged("QtyAvailable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SellerUsername {
            get {
                return this.SellerUsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.SellerUsernameField, value) != true)) {
                    this.SellerUsernameField = value;
                    this.RaisePropertyChanged("SellerUsername");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProductsServiceClient.IProductsService")]
    public interface IProductsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetLatestProducts", ReplyAction="http://tempuri.org/IProductsService/GetLatestProductsResponse")]
        System.Collections.Generic.List<Common.Views.ProductListView> GetLatestProducts(int count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetLatestProducts", ReplyAction="http://tempuri.org/IProductsService/GetLatestProductsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.ProductListView>> GetLatestProductsAsync(int count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetAllProducts", ReplyAction="http://tempuri.org/IProductsService/GetAllProductsResponse")]
        System.Collections.Generic.List<Common.Views.ProductListView> GetAllProducts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetAllProducts", ReplyAction="http://tempuri.org/IProductsService/GetAllProductsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.ProductListView>> GetAllProductsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetProductsByCategory", ReplyAction="http://tempuri.org/IProductsService/GetProductsByCategoryResponse")]
        System.Collections.Generic.List<Common.Views.ProductListView> GetProductsByCategory(System.Guid categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetProductsByCategory", ReplyAction="http://tempuri.org/IProductsService/GetProductsByCategoryResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.ProductListView>> GetProductsByCategoryAsync(System.Guid categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/IsCartQuantityRequestedAvailable", ReplyAction="http://tempuri.org/IProductsService/IsCartQuantityRequestedAvailableResponse")]
        bool IsCartQuantityRequestedAvailable(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/IsCartQuantityRequestedAvailable", ReplyAction="http://tempuri.org/IProductsService/IsCartQuantityRequestedAvailableResponse")]
        System.Threading.Tasks.Task<bool> IsCartQuantityRequestedAvailableAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/AddProductToCart", ReplyAction="http://tempuri.org/IProductsService/AddProductToCartResponse")]
        void AddProductToCart(string username, int productId, int qtyRequested);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/AddProductToCart", ReplyAction="http://tempuri.org/IProductsService/AddProductToCartResponse")]
        System.Threading.Tasks.Task AddProductToCartAsync(string username, int productId, int qtyRequested);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/UpdateCart", ReplyAction="http://tempuri.org/IProductsService/UpdateCartResponse")]
        void UpdateCart(System.Collections.Generic.List<Common.Views.CartItemView> cartItems, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/UpdateCart", ReplyAction="http://tempuri.org/IProductsService/UpdateCartResponse")]
        System.Threading.Tasks.Task UpdateCartAsync(System.Collections.Generic.List<Common.Views.CartItemView> cartItems, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetShoppingCart", ReplyAction="http://tempuri.org/IProductsService/GetShoppingCartResponse")]
        Common.Views.ShoppingCartView GetShoppingCart(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetShoppingCart", ReplyAction="http://tempuri.org/IProductsService/GetShoppingCartResponse")]
        System.Threading.Tasks.Task<Common.Views.ShoppingCartView> GetShoppingCartAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetProductDetails", ReplyAction="http://tempuri.org/IProductsService/GetProductDetailsResponse")]
        Common.Views.ProductDetailsView GetProductDetails(int prodId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetProductDetails", ReplyAction="http://tempuri.org/IProductsService/GetProductDetailsResponse")]
        System.Threading.Tasks.Task<Common.Views.ProductDetailsView> GetProductDetailsAsync(int prodId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetNumberOfItems", ReplyAction="http://tempuri.org/IProductsService/GetNumberOfItemsResponse")]
        int GetNumberOfItems(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductsService/GetNumberOfItems", ReplyAction="http://tempuri.org/IProductsService/GetNumberOfItemsResponse")]
        System.Threading.Tasks.Task<int> GetNumberOfItemsAsync(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProductsServiceChannel : TradersMarketplace.ProductsServiceClient.IProductsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProductsServiceClient : System.ServiceModel.ClientBase<TradersMarketplace.ProductsServiceClient.IProductsService>, TradersMarketplace.ProductsServiceClient.IProductsService {
        
        public ProductsServiceClient() {
        }
        
        public ProductsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProductsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<Common.Views.ProductListView> GetLatestProducts(int count) {
            return base.Channel.GetLatestProducts(count);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.ProductListView>> GetLatestProductsAsync(int count) {
            return base.Channel.GetLatestProductsAsync(count);
        }
        
        public System.Collections.Generic.List<Common.Views.ProductListView> GetAllProducts() {
            return base.Channel.GetAllProducts();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.ProductListView>> GetAllProductsAsync() {
            return base.Channel.GetAllProductsAsync();
        }
        
        public System.Collections.Generic.List<Common.Views.ProductListView> GetProductsByCategory(System.Guid categoryId) {
            return base.Channel.GetProductsByCategory(categoryId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.ProductListView>> GetProductsByCategoryAsync(System.Guid categoryId) {
            return base.Channel.GetProductsByCategoryAsync(categoryId);
        }
        
        public bool IsCartQuantityRequestedAvailable(string username) {
            return base.Channel.IsCartQuantityRequestedAvailable(username);
        }
        
        public System.Threading.Tasks.Task<bool> IsCartQuantityRequestedAvailableAsync(string username) {
            return base.Channel.IsCartQuantityRequestedAvailableAsync(username);
        }
        
        public void AddProductToCart(string username, int productId, int qtyRequested) {
            base.Channel.AddProductToCart(username, productId, qtyRequested);
        }
        
        public System.Threading.Tasks.Task AddProductToCartAsync(string username, int productId, int qtyRequested) {
            return base.Channel.AddProductToCartAsync(username, productId, qtyRequested);
        }
        
        public void UpdateCart(System.Collections.Generic.List<Common.Views.CartItemView> cartItems, string username) {
            base.Channel.UpdateCart(cartItems, username);
        }
        
        public System.Threading.Tasks.Task UpdateCartAsync(System.Collections.Generic.List<Common.Views.CartItemView> cartItems, string username) {
            return base.Channel.UpdateCartAsync(cartItems, username);
        }
        
        public Common.Views.ShoppingCartView GetShoppingCart(string username) {
            return base.Channel.GetShoppingCart(username);
        }
        
        public System.Threading.Tasks.Task<Common.Views.ShoppingCartView> GetShoppingCartAsync(string username) {
            return base.Channel.GetShoppingCartAsync(username);
        }
        
        public Common.Views.ProductDetailsView GetProductDetails(int prodId) {
            return base.Channel.GetProductDetails(prodId);
        }
        
        public System.Threading.Tasks.Task<Common.Views.ProductDetailsView> GetProductDetailsAsync(int prodId) {
            return base.Channel.GetProductDetailsAsync(prodId);
        }
        
        public int GetNumberOfItems(string username) {
            return base.Channel.GetNumberOfItems(username);
        }
        
        public System.Threading.Tasks.Task<int> GetNumberOfItemsAsync(string username) {
            return base.Channel.GetNumberOfItemsAsync(username);
        }
    }
}
