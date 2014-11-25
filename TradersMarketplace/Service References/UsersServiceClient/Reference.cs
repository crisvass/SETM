﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TradersMarketplace.UsersServiceClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UsersServiceClient.IUsersService")]
    public interface IUsersService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/RegisterBuyer", ReplyAction="http://tempuri.org/IUsersService/RegisterBuyerResponse")]
        void RegisterBuyer(string username, string password, string email, string name, string surname, string residence, string street, string town, string postCode, string country, int creditCardTypeId, string cardHolderName, int expiryDateMonth, int expiryDateYear);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/RegisterBuyer", ReplyAction="http://tempuri.org/IUsersService/RegisterBuyerResponse")]
        System.Threading.Tasks.Task RegisterBuyerAsync(string username, string password, string email, string name, string surname, string residence, string street, string town, string postCode, string country, int creditCardTypeId, string cardHolderName, int expiryDateMonth, int expiryDateYear);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/RegisterSeller", ReplyAction="http://tempuri.org/IUsersService/RegisterSellerResponse")]
        void RegisterSeller(string username, string password, string email, string name, string surname, string residence, string street, string town, string postCode, string country, bool requiresDelivery, string ibanNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/RegisterSeller", ReplyAction="http://tempuri.org/IUsersService/RegisterSellerResponse")]
        System.Threading.Tasks.Task RegisterSellerAsync(string username, string password, string email, string name, string surname, string residence, string street, string town, string postCode, string country, bool requiresDelivery, string ibanNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/IsUserAuthenticated", ReplyAction="http://tempuri.org/IUsersService/IsUserAuthenticatedResponse")]
        bool IsUserAuthenticated(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/IsUserAuthenticated", ReplyAction="http://tempuri.org/IUsersService/IsUserAuthenticatedResponse")]
        System.Threading.Tasks.Task<bool> IsUserAuthenticatedAsync(string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsersServiceChannel : TradersMarketplace.UsersServiceClient.IUsersService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsersServiceClient : System.ServiceModel.ClientBase<TradersMarketplace.UsersServiceClient.IUsersService>, TradersMarketplace.UsersServiceClient.IUsersService {
        
        public UsersServiceClient() {
        }
        
        public UsersServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UsersServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void RegisterBuyer(string username, string password, string email, string name, string surname, string residence, string street, string town, string postCode, string country, int creditCardTypeId, string cardHolderName, int expiryDateMonth, int expiryDateYear) {
            base.Channel.RegisterBuyer(username, password, email, name, surname, residence, street, town, postCode, country, creditCardTypeId, cardHolderName, expiryDateMonth, expiryDateYear);
        }
        
        public System.Threading.Tasks.Task RegisterBuyerAsync(string username, string password, string email, string name, string surname, string residence, string street, string town, string postCode, string country, int creditCardTypeId, string cardHolderName, int expiryDateMonth, int expiryDateYear) {
            return base.Channel.RegisterBuyerAsync(username, password, email, name, surname, residence, street, town, postCode, country, creditCardTypeId, cardHolderName, expiryDateMonth, expiryDateYear);
        }
        
        public void RegisterSeller(string username, string password, string email, string name, string surname, string residence, string street, string town, string postCode, string country, bool requiresDelivery, string ibanNumber) {
            base.Channel.RegisterSeller(username, password, email, name, surname, residence, street, town, postCode, country, requiresDelivery, ibanNumber);
        }
        
        public System.Threading.Tasks.Task RegisterSellerAsync(string username, string password, string email, string name, string surname, string residence, string street, string town, string postCode, string country, bool requiresDelivery, string ibanNumber) {
            return base.Channel.RegisterSellerAsync(username, password, email, name, surname, residence, street, town, postCode, country, requiresDelivery, ibanNumber);
        }
        
        public bool IsUserAuthenticated(string username, string password) {
            return base.Channel.IsUserAuthenticated(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> IsUserAuthenticatedAsync(string username, string password) {
            return base.Channel.IsUserAuthenticatedAsync(username, password);
        }
    }
}
