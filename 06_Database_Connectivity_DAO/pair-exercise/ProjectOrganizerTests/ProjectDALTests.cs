using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using ProjectOrganizer;
using System.Data.SqlClient;
using ProjectOrganizer.DAL;
using System.Collections.Generic;
using ProjectOrganizer.Models;
using System;

namespace ProjectOrganizerTests
{
    [TestClass]
    public class ProjectDALTests
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

                string sql_insert = "INSERT INTO project (name, from_date, to_date) VALUES ('ZZZZZ', '2050-01-01', '2050-02-01');";
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
        public void GetAllProjectsTest()
        {
            ProjectSqlDAO access = new ProjectSqlDAO(connectionString);
            IList<Project> items = access.GetAllProjects();

            bool found = false;
            foreach (Project item in items)
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
            ProjectSqlDAO access = new ProjectSqlDAO(connectionString);
            Project temp = new Project();
            temp.Name = "XXXX";
            temp.ProjectId = 50;
            temp.StartDate = new DateTime(2030, 02, 01);
            temp.EndDate = new DateTime(2030, 3, 1);

            int result = access.CreateProject(temp);

            Assert.AreEqual(50, result);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_select = "SELECT * FROM project WHERE name = 'XXXX';";

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
        public void AssignEmployeeToProjectTest()
        {
            ProjectSqlDAO access = new ProjectSqlDAO(connectionString);
//Do we need to add a project and an employee for this, or is this ok?
            bool result = access.AssignEmployeeToProject(6, 1);

            Assert.IsTrue(result);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_select = "SELECT * FROM project_employee WHERE project_id = 6 AND employee_id = 1;";

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
        public void RemoveEmployeeFromProjectTest()
        {
            ProjectSqlDAO access = new ProjectSqlDAO(connectionString);
            //Do we need to add a project and an employee for this, or is this ok?
            bool result = access.RemoveEmployeeFromProject(1, 3);

            Assert.IsTrue(result);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_select = "SELECT * FROM project_employee WHERE project_id = 1 AND employee_id = 3;";

                SqlCommand cmd = new SqlCommand(sql_select, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    count++;
                }

                Assert.AreEqual(0, count);
            }
        }
    }
}
