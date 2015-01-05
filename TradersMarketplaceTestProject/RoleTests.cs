using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TradersMarketplaceTestProject
{
    [TestClass]
    public class RoleTests
    {
        public RolesServiceClient.RolesServiceClient RoleService { get; set; }
        public string ConnectionString { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            RoleService = new RolesServiceClient.RolesServiceClient();
            ConnectionString = "Data Source='CRISTINA-VAIO';Initial Catalog='dbTradersMarketplace';Integrated Security=True";
        }


        public void AreRolesListEqual(List<IdentityRole> expected, List<IdentityRole> actual)
        {
            if (actual.Count != expected.Count)
            {
                Assert.Fail("List are not of the same size.");
            }

            expected = expected.OrderBy(r => r.Id).ToList<IdentityRole>();
            actual = actual.OrderBy(r => r.Id).ToList<IdentityRole>();
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Name, actual[i].Name);
            }
        }
    }
}
