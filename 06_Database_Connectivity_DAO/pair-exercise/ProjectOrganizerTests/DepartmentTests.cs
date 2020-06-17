using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using ProjectOrganizer;
using System.Data.SqlClient;
using ProjectOrganizer.DAL;
using System.Collections.Generic;
using ProjectOrganizer.Models;

namespace ProjectOrganizerTests
{
    [TestClass]
    public class DepartmentTests
    {
        private string connectionString = "Data Source =.\\SQLEXPRESS;Initial Catalog = EmployeeDB; Integrated Security = True";
        private TransactionScope transaction;
        [TestInitialize]
        public void Initialize()
        {
            transaction = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_insert = "INSERT INTO department (name) VALUES ('ZZZZZ')";
                SqlCommand cmd = new SqlCommand(sql_insert, conn);
                int count = cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        [TestMethod]
        public void GetDepartmentsTest()
        {
            DepartmentSqlDAO access = new DepartmentSqlDAO(connectionString);
            IList<Department> items = access.GetDepartments();

            bool found = false;
            foreach (Department item in items)
            {
                if (item.Name == "ZZZZZ")
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void CreateDepartmentTest()
        {
            DepartmentSqlDAO access = new DepartmentSqlDAO(connectionString);
            Department temp = new Department();
            temp.Name = "XXXX";
            temp.Id = 50;

            int result = access.CreateDepartment(temp);

            Assert.AreEqual(50, result);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_select = "SELECT * FROM department WHERE name = 'XXXX';";

                SqlCommand cmd = new SqlCommand(sql_select, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    count++;
                }

                Assert.AreEqual(1, count);
            }
        }
        [TestMethod]
        public void UpdateDepartmentTest()
        {
            DepartmentSqlDAO access = new DepartmentSqlDAO(connectionString);
            Department temp = new Department();
            temp.Name = "YYYY";
            temp.Id = 4;

            bool result = access.UpdateDepartment(temp);

            Assert.IsTrue(result);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_select = "SELECT * FROM department WHERE name = 'YYYY';";

                SqlCommand cmd = new SqlCommand(sql_select, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    count++;
                }

                Assert.AreEqual(1, count);
            }
        }
    }
}
