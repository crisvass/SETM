using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using TradersMarketplaceTestProject.RolesServiceClient;
using Common;
using Common.Views;
using System.Data;
using System.ServiceModel;

namespace TradersMarketplaceTestProject
{
    [TestClass]
    public class RolesServiceTest
    {
        private TransactionScope tScope;
        private RolesServiceClient.RolesServiceClient roleService;

        [TestInitialize]
        public void Initialize()
        {
            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            tScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions);
            roleService = new RolesServiceClient.RolesServiceClient();
        }

        [TestCleanup]
        public void Cleanup()
        {
            tScope.Dispose();
        }

        [TestMethod]
        public void Test_CreateRole_Success()
        {
            string roleName = "TestRole";
            roleService.AddRole(roleName);
            Assert.IsNotNull(roleService.GetRoles().SingleOrDefault(r => r.RoleName == roleName));
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Test_CreateRole_Existing()
        {
            string roleName = "Seller";
            try
            {
                roleService.AddRole(roleName);
            }
            finally
            {
                Assert.AreEqual(roleService.GetRoles().Where(r => r.RoleName == roleName).Count(), 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Test_CreateRole_InvalidName()
        {
            string roleName = "";
            try
            {
                roleService.AddRole(roleName);
            }
            finally
            {
                Assert.IsNull(roleService.GetRoles().SingleOrDefault(r => r.RoleName == roleName));
            }
        }

        [TestMethod]
        public void Test_ReadRole_Success()
        {
            string id = "fc4e4b64-ac4b-4aec-b626-ec2fa6e88d32";
            Assert.IsNotNull(roleService.GetRole(id));
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Test_ReadRole_DoesNotExist()
        {
            string id = "8aeeb638-5bee-48f5-a2e9-7f1f18b74a24";
            RoleView role = null;
            try
            {
                role = roleService.GetRole(id);
            }
            finally
            {
                Assert.IsNull(role);
            }
        }

        [TestMethod]
        public void Test_UpdateRole_Success()
        {
            string id = "9384c793-84df-4576-bddd-f7ed02aae6b9";
            string roleName = "TestingUpdateRole";
            roleService.UpdateRole(id, roleName);
            Assert.AreEqual(roleService.GetRole(id).RoleName, roleName);
        }

        [TestMethod]
        public void Test_DeleteRole_Success()
        {
            string id = "0d09dcda-bbc6-40bb-9398-8c72d46819e6";
            roleService.DeleteRole(id);
            Assert.IsNull(roleService.GetRoles().SingleOrDefault(r => r.RoleId == id));
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException))]
        public void Test_DeleteRole_AssignedRole()
        {
            string id = "fc4e4b64-ac4b-4aec-b626-ec2fa6e88d32";

            try
            {
                roleService.DeleteRole(id);
            }
            finally
            {
                Assert.IsNotNull(roleService.GetRole(id));
            }
        }

        [TestMethod]
        public void Test_ListRoles_Success()
        {
            RoleView[] roles = roleService.GetRoles();
        }
    }
}
