using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.DAL
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public IList<Project> GetAllProjects()
        {
            List<Project> result = new List<Project>();
            string sql_command = "SELECT * FROM project;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_command, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string projectID = Convert.ToString(reader["project_id"]);
                        string projectName = Convert.ToString(reader["name"]);
                        string startDate = Convert.ToString(reader["from_date"]);
                        string finishDate = Convert.ToString(reader["to_date"]);

                        Project temp = new Project();
                        temp.ProjectId = int.Parse(projectID);
                        temp.Name = projectName;
                        temp.StartDate = DateTime.Parse(startDate);
                        temp.EndDate = DateTime.Parse(finishDate);

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
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            string sqlCommand = "INSERT INTO project_employee (employee_id, project_id) VALUES (@employee_id, @project_id);";
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);
                    cmd.Parameters.AddWithValue("@project_id", projectId);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return result;
        
    }

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
        string sqlCommand = "DELETE FROM project_employee WHERE project_id = @project_id AND employee_id = @employee_id";
        bool result = false;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                cmd.Parameters.AddWithValue("@employee_id", employeeId);
                cmd.Parameters.AddWithValue("@project_id", projectId);

                int count = cmd.ExecuteNonQuery();
                //DELETE statements return 0 rows affected
                if (count > 0)
                {
                    result = true;
                }
            }
        }
        catch (Exception e)
        {

        }
        return result;
    }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            string sqlCommand = "INSERT INTO project (name, from_date, to_date) VALUES (@name, @from_date, @to_date);";
            int result = -1;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                    cmd.Parameters.AddWithValue("@name", newProject.Name);
                    cmd.Parameters.AddWithValue("@from_date", newProject.StartDate.Year + "-0" +newProject.StartDate.Month +"-0"+newProject.StartDate.Day);
                    cmd.Parameters.AddWithValue("@to_date", newProject.EndDate.Year + "-0" + newProject.EndDate.Month + "-0" + newProject.EndDate.Day);

                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        result = newProject.ProjectId;
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
