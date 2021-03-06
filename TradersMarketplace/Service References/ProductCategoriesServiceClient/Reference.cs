﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TradersMarketplace.ProductCategoriesServiceClient {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CategoryView", Namespace="http://schemas.datacontract.org/2004/07/Common.Views")]
    [System.SerializableAttribute()]
    public partial class CategoryView : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid CategoryIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CategoryNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<Common.Views.CategoryView> SubCategoriesField;
        
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
        public System.Guid CategoryId {
            get {
                return this.CategoryIdField;
            }
            set {
                if ((this.CategoryIdField.Equals(value) != true)) {
                    this.CategoryIdField = value;
                    this.RaisePropertyChanged("CategoryId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CategoryName {
            get {
                return this.CategoryNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CategoryNameField, value) != true)) {
                    this.CategoryNameField = value;
                    this.RaisePropertyChanged("CategoryName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<Common.Views.CategoryView> SubCategories {
            get {
                return this.SubCategoriesField;
            }
            set {
                if ((object.ReferenceEquals(this.SubCategoriesField, value) != true)) {
                    this.SubCategoriesField = value;
                    this.RaisePropertyChanged("SubCategories");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProductCategoriesServiceClient.IProductCategoriesService")]
    public interface IProductCategoriesService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetCategories", ReplyAction="http://tempuri.org/IProductCategoriesService/GetCategoriesResponse")]
        System.Collections.Generic.List<Common.Views.CategoryView> GetCategories();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetCategories", ReplyAction="http://tempuri.org/IProductCategoriesService/GetCategoriesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.CategoryView>> GetCategoriesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetCategoryName", ReplyAction="http://tempuri.org/IProductCategoriesService/GetCategoryNameResponse")]
        string GetCategoryName(System.Guid categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetCategoryName", ReplyAction="http://tempuri.org/IProductCategoriesService/GetCategoryNameResponse")]
        System.Threading.Tasks.Task<string> GetCategoryNameAsync(System.Guid categoryId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetCategory", ReplyAction="http://tempuri.org/IProductCategoriesService/GetCategoryResponse")]
        Common.Views.CategoryView GetCategory(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetCategory", ReplyAction="http://tempuri.org/IProductCategoriesService/GetCategoryResponse")]
        System.Threading.Tasks.Task<Common.Views.CategoryView> GetCategoryAsync(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/AddCategory", ReplyAction="http://tempuri.org/IProductCategoriesService/AddCategoryResponse")]
        void AddCategory(string categoryName, System.Collections.Generic.List<Common.Views.CategoryView> subCategories);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/AddCategory", ReplyAction="http://tempuri.org/IProductCategoriesService/AddCategoryResponse")]
        System.Threading.Tasks.Task AddCategoryAsync(string categoryName, System.Collections.Generic.List<Common.Views.CategoryView> subCategories);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/UpdateCategory", ReplyAction="http://tempuri.org/IProductCategoriesService/UpdateCategoryResponse")]
        void UpdateCategory(System.Guid categoryId, string categoryName, System.Collections.Generic.List<Common.Views.CategoryView> subCategories);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/UpdateCategory", ReplyAction="http://tempuri.org/IProductCategoriesService/UpdateCategoryResponse")]
        System.Threading.Tasks.Task UpdateCategoryAsync(System.Guid categoryId, string categoryName, System.Collections.Generic.List<Common.Views.CategoryView> subCategories);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/DeleteCategory", ReplyAction="http://tempuri.org/IProductCategoriesService/DeleteCategoryResponse")]
        void DeleteCategory(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/DeleteCategory", ReplyAction="http://tempuri.org/IProductCategoriesService/DeleteCategoryResponse")]
        System.Threading.Tasks.Task DeleteCategoryAsync(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetMainCategories", ReplyAction="http://tempuri.org/IProductCategoriesService/GetMainCategoriesResponse")]
        System.Collections.Generic.List<Common.Views.CategoryView> GetMainCategories();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetMainCategories", ReplyAction="http://tempuri.org/IProductCategoriesService/GetMainCategoriesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.CategoryView>> GetMainCategoriesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetSubcategories", ReplyAction="http://tempuri.org/IProductCategoriesService/GetSubcategoriesResponse")]
        System.Collections.Generic.List<Common.Views.CategoryView> GetSubcategories(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductCategoriesService/GetSubcategories", ReplyAction="http://tempuri.org/IProductCategoriesService/GetSubcategoriesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.CategoryView>> GetSubcategoriesAsync(System.Guid id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProductCategoriesServiceChannel : TradersMarketplace.ProductCategoriesServiceClient.IProductCategoriesService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProductCategoriesServiceClient : System.ServiceModel.ClientBase<TradersMarketplace.ProductCategoriesServiceClient.IProductCategoriesService>, TradersMarketplace.ProductCategoriesServiceClient.IProductCategoriesService {
        
        public ProductCategoriesServiceClient() {
        }
        
        public ProductCategoriesServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProductCategoriesServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductCategoriesServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductCategoriesServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<Common.Views.CategoryView> GetCategories() {
            return base.Channel.GetCategories();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.CategoryView>> GetCategoriesAsync() {
            return base.Channel.GetCategoriesAsync();
        }
        
        public string GetCategoryName(System.Guid categoryId) {
            return base.Channel.GetCategoryName(categoryId);
        }
        
        public System.Threading.Tasks.Task<string> GetCategoryNameAsync(System.Guid categoryId) {
            return base.Channel.GetCategoryNameAsync(categoryId);
        }
        
        public Common.Views.CategoryView GetCategory(System.Guid id) {
            return base.Channel.GetCategory(id);
        }
        
        public System.Threading.Tasks.Task<Common.Views.CategoryView> GetCategoryAsync(System.Guid id) {
            return base.Channel.GetCategoryAsync(id);
        }
        
        public void AddCategory(string categoryName, System.Collections.Generic.List<Common.Views.CategoryView> subCategories) {
            base.Channel.AddCategory(categoryName, subCategories);
        }
        
        public System.Threading.Tasks.Task AddCategoryAsync(string categoryName, System.Collections.Generic.List<Common.Views.CategoryView> subCategories) {
            return base.Channel.AddCategoryAsync(categoryName, subCategories);
        }
        
        public void UpdateCategory(System.Guid categoryId, string categoryName, System.Collections.Generic.List<Common.Views.CategoryView> subCategories) {
            base.Channel.UpdateCategory(categoryId, categoryName, subCategories);
        }
        
        public System.Threading.Tasks.Task UpdateCategoryAsync(System.Guid categoryId, string categoryName, System.Collections.Generic.List<Common.Views.CategoryView> subCategories) {
            return base.Channel.UpdateCategoryAsync(categoryId, categoryName, subCategories);
        }
        
        public void DeleteCategory(System.Guid id) {
            base.Channel.DeleteCategory(id);
        }
        
        public System.Threading.Tasks.Task DeleteCategoryAsync(System.Guid id) {
            return base.Channel.DeleteCategoryAsync(id);
        }
        
        public System.Collections.Generic.List<Common.Views.CategoryView> GetMainCategories() {
            return base.Channel.GetMainCategories();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.CategoryView>> GetMainCategoriesAsync() {
            return base.Channel.GetMainCategoriesAsync();
        }
        
        public System.Collections.Generic.List<Common.Views.CategoryView> GetSubcategories(System.Guid id) {
            return base.Channel.GetSubcategories(id);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Common.Views.CategoryView>> GetSubcategoriesAsync(System.Guid id) {
            return base.Channel.GetSubcategoriesAsync(id);
        }
    }
}
