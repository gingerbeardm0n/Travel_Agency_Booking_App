using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.DAL
{
    public class EmployeeSqlDAO : IEmployeeDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public EmployeeSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public IList<Employee> GetAllEmployees()
        {
            List<Employee> result = new List<Employee>();
            string sql_command = "SELECT * FROM employee;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_command, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string departmentID = Convert.ToString(reader["department_id"]);
                        string firstName = Convert.ToString(reader["first_name"]);
                        string lastName = Convert.ToString(reader["last_name"]);
                        string jobTitle = Convert.ToString(reader["job_title"]);
                        string birthDate = Convert.ToString(reader["birth_date"]);
                        string gender = Convert.ToString(reader["gender"]);
                        string hireDate = Convert.ToString(reader["hire_date"]);
                        string employeeID = Convert.ToString(reader["employee_id"]);

                        Employee temp = new Employee();
                        temp.BirthDate = DateTime.Parse(birthDate);
                        //temp.BirthDate = temp.S
                        temp.HireDate = DateTime.Parse(hireDate); 
                        temp.EmployeeId = int.Parse(employeeID);
                        temp.DepartmentId = int.Parse(departmentID);
                        temp.FirstName = firstName;
                        temp.LastName = lastName;
                        temp.JobTitle = jobTitle;
                        temp.Gender = gender;

                        result.Add(temp);
                    }
                }

            }
            catch (Exception e)
            {

            }
            return result;
        }

        /// <summary>
        /// Searches the system for an employee by first name or last name.
        /// </summary>
        /// <remarks>The search performed is a wildcard search.</remarks>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns>A list of employees that match the search.</returns>
        public IList<Employee> Search(string firstname, string lastname)
        {
            List<Employee> result = new List<Employee>();
            string sql_command = "SELECT * FROM employee WHERE first_name = @first_name OR last_name = @last_name;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_command, conn);


                    cmd.Parameters.AddWithValue("@first_name", firstname);
                    cmd.Parameters.AddWithValue("@last_name", lastname);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string departmentID = Convert.ToString(reader["department_id"]);
                        string firstName = Convert.ToString(reader["first_name"]);
                        string lastName = Convert.ToString(reader["last_name"]);
                        string jobTitle = Convert.ToString(reader["job_title"]);
                        string birthDate = Convert.ToString(reader["birth_date"]);
                        string gender = Convert.ToString(reader["gender"]);
                        string hireDate = Convert.ToString(reader["hire_date"]);
                        string employeeID = Convert.ToString(reader["employee_id"]);

                        Employee temp = new Employee();
                        temp.BirthDate = DateTime.Parse(birthDate);
                        temp.HireDate = DateTime.Parse(hireDate); 
                        temp.EmployeeId = int.Parse(employeeID);
                        temp.DepartmentId = int.Parse(departmentID);
                        temp.FirstName = firstName;
                        temp.LastName = lastName;
                        temp.JobTitle = jobTitle;
                        temp.Gender = gender;

                        result.Add(temp);
                    }
                }

            }
            catch (Exception e)
            {

            }
            return result;
        }

        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> result = new List<Employee>();
            string sql_command = "SELECT * FROM employee e LEFT JOIN project_employee pe ON e.employee_id = pe.employee_id WHERE pe.project_id IS NULL;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_command, conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string departmentID = Convert.ToString(reader["department_id"]);
                        string firstName = Convert.ToString(reader["first_name"]);
                        string lastName = Convert.ToString(reader["last_name"]);
                        string jobTitle = Convert.ToString(reader["job_title"]);
                        string birthDate = Convert.ToString(reader["birth_date"]);
                        string gender = Convert.ToString(reader["gender"]);
                        string hireDate = Convert.ToString(reader["hire_date"]);
                        string employeeID = Convert.ToString(reader["employee_id"]);

                        Employee temp = new Employee();
                        temp.BirthDate = DateTime.Parse(birthDate); //I believe i need to make a change here
                        temp.HireDate = DateTime.Parse(hireDate); //I believe i need to make a change here
                        temp.EmployeeId = int.Parse(employeeID);
                        temp.DepartmentId = int.Parse(departmentID);
                        temp.FirstName = firstName;
                        temp.LastName = lastName;
                        temp.JobTitle = jobTitle;
                        temp.Gender = gender;

                        result.Add(temp);
                    }
                }

            }
            catch (Exception e)
            {

            }
            return result;
        }
    }
}

