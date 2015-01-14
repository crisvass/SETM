using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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

            //create snapshot
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(
                    "CREATE DATABASE dbTradersMarketplace_Testing_dbss ON (NAME = dbTradersMarketplace, FILENAME = " +
                    "'C:\\Program Files\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\Data\\dbTradersMarketplace_Testing_dbss.ss' )" +
                    "AS SNAPSHOT OF dbTradersMarketplace;", conn))
                {
                    comm.ExecuteNonQuery();
                }
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                //kill open connections
                using (SqlCommand comm = new SqlCommand(
                    "USE master; SET NOCOUNT ON DECLARE @DBName varchar(50) DECLARE @spidstr varchar(8000) " +
                    "DECLARE @ConnKilled smallint SET @ConnKilled=0 SET @spidstr = '' Set @DBName = 'dbTradersMarketplace' " +
                    "IF db_id(@DBName) < 4 BEGIN PRINT 'Connections to system databases cannot be killed' RETURN END " +
                    "SELECT @spidstr=coalesce(@spidstr,',' )+'kill '+convert(varchar, spid)+ '; ' " +
                    "FROM master..sysprocesses WHERE dbid=db_id(@DBName) " +
                    "IF LEN(@spidstr) > 0 BEGIN EXEC(@spidstr) SELECT @ConnKilled = COUNT(1) " +
                    "FROM master..sysprocesses WHERE dbid=db_id(@DBName) END"
                            , conn))
                {
                    comm.ExecuteNonQuery();
                }

                //restore database
                using (SqlCommand comm = new SqlCommand(
                    "USE master; RESTORE DATABASE dbTradersMarketplace FROM DATABASE_SNAPSHOT = 'dbTradersMarketplace_Testing_dbss';"
                            , conn))
                {
                    comm.ExecuteNonQuery();
                }

                //drop snapshot
                using (SqlCommand comm = new SqlCommand(
                    "DROP DATABASE dbTradersMarketplace_Testing_dbss;", conn))
                {
                    comm.ExecuteNonQuery();
                }
            }
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
