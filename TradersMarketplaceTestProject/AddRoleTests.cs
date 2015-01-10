using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Common;
using Common.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradersMarketplaceTestProject.RolesServiceClient;

namespace TradersMarketplaceTestProject
{
    [TestClass]
    public class AddRoleTests : RoleTests
    {
        public AddRoleTests() : base() { }

        [TestMethod]
        public void Test1_AddRole()
        {
            IdentityRole addedRole = null;
            try
            {
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

                    string roleName = "R";
                    addedRole = RoleService.AddRole(roleName);

                    List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                    //expected: rolesBefore + new role
                    expectedRolesAfter.AddRange(rolesBefore);
                    IdentityRole expectedRoleAfter = new IdentityRole()
                    {
                        Id = addedRole.Id,
                        Name = roleName
                    }; expectedRolesAfter.Add(expectedRoleAfter);

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
            finally
            {
                if (addedRole != null)
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        int i = 0;
                        using (SqlCommand comm = new SqlCommand("DELETE FROM IdentityRoles WHERE Id = '" + addedRole.Id + "'", conn))
                        {
                            i = comm.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void Test2_AddRole()
        {
            IdentityRole addedRole = null;
            try
            {
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

                    string roleName = "TestRoleTestRoleTestRoleT";
                    addedRole = RoleService.AddRole(roleName);

                    List<IdentityRole> expectedRolesAfter = new List<IdentityRole>();
                    //expected: rolesBefore + new role
                    expectedRolesAfter.AddRange(rolesBefore);
                    IdentityRole expectedRoleAfter = new IdentityRole()
                    {
                        Id = addedRole.Id,
                        Name = roleName
                    }; expectedRolesAfter.Add(expectedRoleAfter);

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
            finally
            {
                if (addedRole != null)
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        int i = 0;
                        using (SqlCommand comm = new SqlCommand("DELETE FROM IdentityRoles WHERE Id = '" + addedRole.Id + "'", conn))
                        {
                            i = comm.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role name must be unique.")]
        public void Test3_AddRole()
        {
            IdentityRole addedRole = null;
            try
            {
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

                    string roleName = "Buyer";
                    addedRole = RoleService.AddRole(roleName);

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
            finally
            {
                if (addedRole != null)
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        int i = 0;
                        using (SqlCommand comm = new SqlCommand("DELETE FROM IdentityRoles WHERE Id = '" + addedRole.Id + "'", conn))
                        {
                            i = comm.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role name cannot be null or empty.")]
        public void Test4_AddRole()
        {
            IdentityRole addedRole = null;
            try
            {
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

                    string roleName = null;
                    addedRole = RoleService.AddRole(roleName);

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
            finally
            {
                if (addedRole != null)
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        int i = 0;
                        using (SqlCommand comm = new SqlCommand("DELETE FROM IdentityRoles WHERE Id = '" + addedRole.Id + "'", conn))
                        {
                            i = comm.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role name cannot be null or empty.")]
        public void Test5_AddRole()
        {
            IdentityRole addedRole = null;
            try
            {
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

                    string roleName = string.Empty;
                    addedRole = RoleService.AddRole(roleName);

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
            finally
            {
                if (addedRole != null)
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        int i = 0;
                        using (SqlCommand comm = new SqlCommand("DELETE FROM IdentityRoles WHERE Id = '" + addedRole.Id + "'", conn))
                        {
                            i = comm.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role name can contain only alphabet letters.")]
        public void Test6_AddRole()
        {
            IdentityRole addedRole = null;
            try
            {
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

                    string roleName = "*";
                    addedRole = RoleService.AddRole(roleName);

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
            finally
            {
                if (addedRole != null)
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        int i = 0;
                        using (SqlCommand comm = new SqlCommand("DELETE FROM IdentityRoles WHERE Id = '" + addedRole.Id + "'", conn))
                        {
                            i = comm.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role name can contain only alphabet letters.")]
        public void Test7_AddRole()
        {
            IdentityRole addedRole = null;
            try
            {
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

                    string roleName = "Test*Role*Test*Role*Test*";
                    addedRole = RoleService.AddRole(roleName);

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
            finally
            {
                if (addedRole != null)
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        int i = 0;
                        using (SqlCommand comm = new SqlCommand("DELETE FROM IdentityRoles WHERE Id = '" + addedRole.Id + "'", conn))
                        {
                            i = comm.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException), "Role name must be between 1 and 25 characters long.")]
        public void Test8_AddRole()
        {
            IdentityRole addedRole = null;
            try
            {
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

                    string roleName = "TestRoleTestRoleTestRoleTe";
                    addedRole = RoleService.AddRole(roleName);

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
            finally
            {
                if (addedRole != null)
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        int i = 0;
                        using (SqlCommand comm = new SqlCommand("DELETE FROM IdentityRoles WHERE Id = '" + addedRole.Id + "'", conn))
                        {
                            i = comm.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
