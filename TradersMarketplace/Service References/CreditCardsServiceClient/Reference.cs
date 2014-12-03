﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TradersMarketplace.CreditCardsServiceClient {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CreditCardTypeView", Namespace="http://schemas.datacontract.org/2004/07/Common.Views")]
    [System.SerializableAttribute()]
    public partial class CreditCardTypeView : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreditCardTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CreditCardTypeIdField;
        
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
        public string CreditCardType {
            get {
                return this.CreditCardTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.CreditCardTypeField, value) != true)) {
                    this.CreditCardTypeField = value;
                    this.RaisePropertyChanged("CreditCardType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CreditCardTypeId {
            get {
                return this.CreditCardTypeIdField;
            }
            set {
                if ((this.CreditCardTypeIdField.Equals(value) != true)) {
                    this.CreditCardTypeIdField = value;
                    this.RaisePropertyChanged("CreditCardTypeId");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CreditCardsServiceClient.ICreditCardsService")]
    public interface ICreditCardsService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditCardsService/GetCreditCardTypes", ReplyAction="http://tempuri.org/ICreditCardsService/GetCreditCardTypesResponse")]
        System.Collections.ObjectModel.ObservableCollection<TradersMarketplace.CreditCardsServiceClient.CreditCardTypeView> GetCreditCardTypes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditCardsService/GetCreditCardTypes", ReplyAction="http://tempuri.org/ICreditCardsService/GetCreditCardTypesResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<TradersMarketplace.CreditCardsServiceClient.CreditCardTypeView>> GetCreditCardTypesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICreditCardsServiceChannel : TradersMarketplace.CreditCardsServiceClient.ICreditCardsService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CreditCardsServiceClient : System.ServiceModel.ClientBase<TradersMarketplace.CreditCardsServiceClient.ICreditCardsService>, TradersMarketplace.CreditCardsServiceClient.ICreditCardsService {
        
        public CreditCardsServiceClient() {
        }
        
        public CreditCardsServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CreditCardsServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditCardsServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditCardsServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.ObjectModel.ObservableCollection<TradersMarketplace.CreditCardsServiceClient.CreditCardTypeView> GetCreditCardTypes() {
            return base.Channel.GetCreditCardTypes();
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<TradersMarketplace.CreditCardsServiceClient.CreditCardTypeView>> GetCreditCardTypesAsync() {
            return base.Channel.GetCreditCardTypesAsync();
        }
    }
}
