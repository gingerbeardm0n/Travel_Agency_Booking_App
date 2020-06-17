using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Data.SqlClient;
using ProjectOrganizer.DAL;
using ProjectOrganizer.Models;

namespace ProjectOrganizerTests
{
    [TestClass]
    public class EmployeeSqlDAO_Tests
    {
        private string connectionString = "Data Source =.\\SQLEXPRESS;Initial Catalog = EmployeeDB; Integrated Security = True";
        private TransactionScope transaction;

        [TestInitialize]
        public void Init()
        {
            transaction = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_insert = "INSERT INTO employee (department_id, first_name, last_name, job_title, birth_date, gender, hire_date) " +
                    "VALUES (3, 'JoEli', 'Collinsall', 'Master of the Universe', '2042-06-06', 'M', '2043-01-01')";
                SqlCommand cmd = new SqlCommand(sql_insert, conn);
                int count = cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void CleanThisMess()
        {
            transaction.Dispose();
        }

        [TestMethod]
        public void GetAllEmployees_Test()
        {
            EmployeeSqlDAO accessObj = new EmployeeSqlDAO(connectionString);
            IList<Employee> items = accessObj.GetAllEmployees();

            bool found = false;

            foreach (Employee loopItem in items)
            {
                if (loopItem.LastName == "Collinsall")
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void Search_Test()
        {
            EmployeeSqlDAO accessObj = new EmployeeSqlDAO(connectionString);
            IList<Employee> listofMatchedEmployess = accessObj.Search("JoEli", "Collinsall");

            Assert.AreEqual(1, listofMatchedEmployess.Count);
            
        }

        [TestMethod]
        public void GetEmployeesWithoutProjects_Test()
        {
            EmployeeSqlDAO accessObj = new EmployeeSqlDAO(connectionString);
            IList<Employee> listOfEmployeesToFire = accessObj.GetEmployeesWithoutProjects();

            Assert.AreEqual(3, listOfEmployeesToFire.Count);

        }
    }
}
