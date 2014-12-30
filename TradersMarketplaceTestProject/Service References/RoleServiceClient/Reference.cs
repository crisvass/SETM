﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TradersMarketplaceTestProject.RoleServiceClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RoleServiceClient.IRolesService")]
    public interface IRolesService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetUserRoles", ReplyAction="http://tempuri.org/IRolesService/GetUserRolesResponse")]
        Common.Views.RoleView[] GetUserRoles(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetUserRoles", ReplyAction="http://tempuri.org/IRolesService/GetUserRolesResponse")]
        System.Threading.Tasks.Task<Common.Views.RoleView[]> GetUserRolesAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetRoles", ReplyAction="http://tempuri.org/IRolesService/GetRolesResponse")]
        Common.Views.RoleView[] GetRoles();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetRoles", ReplyAction="http://tempuri.org/IRolesService/GetRolesResponse")]
        System.Threading.Tasks.Task<Common.Views.RoleView[]> GetRolesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/AddRole", ReplyAction="http://tempuri.org/IRolesService/AddRoleResponse")]
        void AddRole(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/AddRole", ReplyAction="http://tempuri.org/IRolesService/AddRoleResponse")]
        System.Threading.Tasks.Task AddRoleAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/UpdateRole", ReplyAction="http://tempuri.org/IRolesService/UpdateRoleResponse")]
        void UpdateRole(string id, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/UpdateRole", ReplyAction="http://tempuri.org/IRolesService/UpdateRoleResponse")]
        System.Threading.Tasks.Task UpdateRoleAsync(string id, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetRole", ReplyAction="http://tempuri.org/IRolesService/GetRoleResponse")]
        Common.Views.RoleView GetRole(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetRole", ReplyAction="http://tempuri.org/IRolesService/GetRoleResponse")]
        System.Threading.Tasks.Task<Common.Views.RoleView> GetRoleAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/DeleteRole", ReplyAction="http://tempuri.org/IRolesService/DeleteRoleResponse")]
        void DeleteRole(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/DeleteRole", ReplyAction="http://tempuri.org/IRolesService/DeleteRoleResponse")]
        System.Threading.Tasks.Task DeleteRoleAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetNonMenuAssignedRoles", ReplyAction="http://tempuri.org/IRolesService/GetNonMenuAssignedRolesResponse")]
        Common.Views.RoleView[] GetNonMenuAssignedRoles(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetNonMenuAssignedRoles", ReplyAction="http://tempuri.org/IRolesService/GetNonMenuAssignedRolesResponse")]
        System.Threading.Tasks.Task<Common.Views.RoleView[]> GetNonMenuAssignedRolesAsync(System.Guid id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetNonUserAssignedRoles", ReplyAction="http://tempuri.org/IRolesService/GetNonUserAssignedRolesResponse")]
        Common.Views.RoleView[] GetNonUserAssignedRoles(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRolesService/GetNonUserAssignedRoles", ReplyAction="http://tempuri.org/IRolesService/GetNonUserAssignedRolesResponse")]
        System.Threading.Tasks.Task<Common.Views.RoleView[]> GetNonUserAssignedRolesAsync(string id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRolesServiceChannel : TradersMarketplaceTestProject.RoleServiceClient.IRolesService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RolesServiceClient : System.ServiceModel.ClientBase<TradersMarketplaceTestProject.RoleServiceClient.IRolesService>, TradersMarketplaceTestProject.RoleServiceClient.IRolesService {
        
        public RolesServiceClient() {
        }
        
        public RolesServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RolesServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RolesServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RolesServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Common.Views.RoleView[] GetUserRoles(string username) {
            return base.Channel.GetUserRoles(username);
        }
        
        public System.Threading.Tasks.Task<Common.Views.RoleView[]> GetUserRolesAsync(string username) {
            return base.Channel.GetUserRolesAsync(username);
        }
        
        public Common.Views.RoleView[] GetRoles() {
            return base.Channel.GetRoles();
        }
        
        public System.Threading.Tasks.Task<Common.Views.RoleView[]> GetRolesAsync() {
            return base.Channel.GetRolesAsync();
        }
        
        public void AddRole(string name) {
            base.Channel.AddRole(name);
        }
        
        public System.Threading.Tasks.Task AddRoleAsync(string name) {
            return base.Channel.AddRoleAsync(name);
        }
        
        public void UpdateRole(string id, string name) {
            base.Channel.UpdateRole(id, name);
        }
        
        public System.Threading.Tasks.Task UpdateRoleAsync(string id, string name) {
            return base.Channel.UpdateRoleAsync(id, name);
        }
        
        public Common.Views.RoleView GetRole(string id) {
            return base.Channel.GetRole(id);
        }
        
        public System.Threading.Tasks.Task<Common.Views.RoleView> GetRoleAsync(string id) {
            return base.Channel.GetRoleAsync(id);
        }
        
        public void DeleteRole(string id) {
            base.Channel.DeleteRole(id);
        }
        
        public System.Threading.Tasks.Task DeleteRoleAsync(string id) {
            return base.Channel.DeleteRoleAsync(id);
        }
        
        public Common.Views.RoleView[] GetNonMenuAssignedRoles(System.Guid id) {
            return base.Channel.GetNonMenuAssignedRoles(id);
        }
        
        public System.Threading.Tasks.Task<Common.Views.RoleView[]> GetNonMenuAssignedRolesAsync(System.Guid id) {
            return base.Channel.GetNonMenuAssignedRolesAsync(id);
        }
        
        public Common.Views.RoleView[] GetNonUserAssignedRoles(string id) {
            return base.Channel.GetNonUserAssignedRoles(id);
        }
        
        public System.Threading.Tasks.Task<Common.Views.RoleView[]> GetNonUserAssignedRolesAsync(string id) {
            return base.Channel.GetNonUserAssignedRolesAsync(id);
        }
    }
}
