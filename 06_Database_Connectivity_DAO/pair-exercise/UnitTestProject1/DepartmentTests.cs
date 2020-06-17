using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Transactions;
using ProjectOrganizer;

namespace ProjectOrganizerTests
{
    [TestClass]
    public class DepartmentTests
    {
        private string connectionString = "Data Source =.\\SQLEXPRESS;Initial Catalog = EmployeeDB; Integrated Security = True"
        private TransactionScope transaction;
        [TestInitialize]
        public void Initialize()
        {
            transaction = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sql_insert = "INSERT INTO department (department_id, name) VALUES ('50', 'ZZZZZ')";
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
            Departm
        }
    }
}
