using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TradersMarketplaceTestProject
{
    [TestClass]
    public class DeleteRoleTests : RoleTests
    {
        public DeleteRoleTests() : base() { }

        [TestMethod]
        public void Test1_DeleteRole()
        {
            string id = "afa8aa18-bd12-42c4-82b5-25f036815c89";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                List<IdentityRole> rolesBefore = new List<IdentityRole>();
                using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                {
                    using (SqlDataReader sqlReader = comm.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            IdentityRole tempRole = new IdentityRole()
                            {
                                Id = sqlReader.GetString(0),
                                Name = sqlReader.GetString(1)
                            };
                            rolesBefore.Add(tempRole);
                        }
                    }
                }

                RoleService.DeleteRole(id);

                List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                foreach (IdentityRole r in rolesBefore)
                {
                    if (!r.Id.Equals(id))
                        expectedRolesAfter.Add(r);
                }

                List<IdentityRole> rolesAfter = new List<IdentityRole>();
                using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                {
                    using (SqlDataReader sqlReader = comm.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            IdentityRole tempRole = new IdentityRole()
                            {
                                Id = sqlReader.GetString(0),
                                Name = sqlReader.GetString(1)
                            };
                            rolesAfter.Add(tempRole);
                        }
                    }
                }

                AreRolesListEqual(expectedRolesAfter, rolesAfter);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role ID cannot be null or empty.")]
        public void Test2_DeleteRole()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                List<IdentityRole> rolesBefore = new List<IdentityRole>();
                try
                {
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesBefore.Add(tempRole);
                            }
                        }
                    }

                    string id = null;
                    RoleService.DeleteRole(id);
                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                finally
                {
                    List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                    expectedRolesAfter.AddRange(rolesBefore);

                    List<IdentityRole> rolesAfter = new List<IdentityRole>();
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesAfter.Add(tempRole);
                            }
                        }
                    }

                    AreRolesListEqual(expectedRolesAfter, rolesAfter);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role ID cannot be null or empty.")]
        public void Test3_DeleteRole()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                List<IdentityRole> rolesBefore = new List<IdentityRole>();
                try
                {
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesBefore.Add(tempRole);
                            }
                        }
                    }

                    string id = string.Empty;
                    RoleService.DeleteRole(id);

                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                finally
                {
                    List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                    expectedRolesAfter.AddRange(rolesBefore);

                    List<IdentityRole> rolesAfter = new List<IdentityRole>();
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesAfter.Add(tempRole);
                            }
                        }
                    }

                    AreRolesListEqual(expectedRolesAfter, rolesAfter);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role cannot be deleted because it is assigned to a user.")]
        public void Test4_DeleteRole()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                List<IdentityRole> rolesBefore = new List<IdentityRole>();
                try
                {
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesBefore.Add(tempRole);
                            }
                        }
                    }

                    string id = "fc4e4b64-ac4b-4aec-b626-ec2fa6e88d32";
                    RoleService.DeleteRole(id);
                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                finally
                {
                    List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                    expectedRolesAfter.AddRange(rolesBefore);

                    List<IdentityRole> rolesAfter = new List<IdentityRole>();
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesAfter.Add(tempRole);
                            }
                        }
                    }

                    AreRolesListEqual(expectedRolesAfter, rolesAfter);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role ID does not exist.")]
        public void Test5_DeleteRole()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                List<IdentityRole> rolesBefore = new List<IdentityRole>();
                try
                {
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesBefore.Add(tempRole);
                            }
                        }
                    }

                    string id = "fd318f6d-b10f-4729-b1ab-88eed41249e0";
                    RoleService.DeleteRole(id);

                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                finally
                {
                    List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                    expectedRolesAfter.AddRange(rolesBefore);

                    List<IdentityRole> rolesAfter = new List<IdentityRole>();
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesAfter.Add(tempRole);
                            }
                        }
                    }

                    AreRolesListEqual(expectedRolesAfter, rolesAfter);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role ID cannot be an empty GUID.")]
        public void Test6_DeleteRole()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                List<IdentityRole> rolesBefore = new List<IdentityRole>();
                try
                {
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesBefore.Add(tempRole);
                            }
                        }
                    }

                    string id = "00000000-0000-0000-0000-000000000000";
                    RoleService.DeleteRole(id);
                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                finally
                {
                    List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                    expectedRolesAfter.AddRange(rolesBefore);

                    List<IdentityRole> rolesAfter = new List<IdentityRole>();
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesAfter.Add(tempRole);
                            }
                        }
                    }

                    AreRolesListEqual(expectedRolesAfter, rolesAfter);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role ID was not a valid GUID value.")]
        public void Test7_DeleteRole()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                List<IdentityRole> rolesBefore = new List<IdentityRole>();
                try
                {
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesBefore.Add(tempRole);
                            }
                        }
                    }

                    string id = "739a60f3-d9f9-44c0-b1b-431744333289";
                    RoleService.DeleteRole(id);

                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                finally
                {
                    List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                    expectedRolesAfter.AddRange(rolesBefore);

                    List<IdentityRole> rolesAfter = new List<IdentityRole>();
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesAfter.Add(tempRole);
                            }
                        }
                    }

                    AreRolesListEqual(expectedRolesAfter, rolesAfter);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role ID was not a valid GUID value.")]
        public void Test8_DeleteRole()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                List<IdentityRole> rolesBefore = new List<IdentityRole>();
                try
                {
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesBefore.Add(tempRole);
                            }
                        }
                    }

                    string id = "739a60f3-d9f9-44c0-b1ba-4317443332895";
                    RoleService.DeleteRole(id);
                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                finally
                {
                    List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                    expectedRolesAfter.AddRange(rolesBefore);

                    List<IdentityRole> rolesAfter = new List<IdentityRole>();
                    using (SqlCommand comm = new SqlCommand("SELECT * FROM IdentityRoles", conn))
                    {
                        using (SqlDataReader sqlReader = comm.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                IdentityRole tempRole = new IdentityRole()
                                {
                                    Id = sqlReader.GetString(0),
                                    Name = sqlReader.GetString(1)
                                };
                                rolesAfter.Add(tempRole);
                            }
                        }
                    }

                    AreRolesListEqual(expectedRolesAfter, rolesAfter);
                }
            }
        }
    }
}
