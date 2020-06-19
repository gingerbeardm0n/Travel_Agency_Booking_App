using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class VenuesDAL
    {
        private string connectionString;
        public VenuesDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Venue GetSpecificVenue(string venueName)
        {
            Venue result = new Venue();
            string sql_command = "SELECT v.id, v.name, ci.name city, ci.state_abbreviation state, ca.name category, v.description FROM venue v "
            +"JOIN city ci ON v.city_id = ci.id "
            + "JOIN state s ON ci.state_abbreviation = s.abbreviation "
            + "LEFT JOIN category_venue cv ON v.id = cv.venue_id "
            + "LEFT JOIN category ca ON cv.category_id = ca.id "
            + "WHERE v.name = @venue_name;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql_command, conn);
                    cmd.Parameters.AddWithValue("@venue_name", venueName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int loopCounter = 1;
                    while (reader.Read())
                    {
                        if(loopCounter == 1)
                        {
                            string venueID = Convert.ToString(reader["id"]);
                            string nameOfVenue = Convert.ToString(reader["name"]);
                            string city = Convert.ToString(reader["name"]);
                            string state = Convert.ToString(reader["state"]);
                            string description = Convert.ToString(reader["description"]);
                            result.VenueID = int.Parse(venueID);
                            result.VenueName = nameOfVenue;
                            result.City = city;
                            result.State = state;
                            result.Description = description;
                        }
                        string category;
                             if (DBNull.Value.Equals(reader["category"]))
                        {
                            category = "No Categories Listed, It is What You Make It.";
                        }
                        else
                        {
                            category = Convert.ToString(reader["category"]);
                        }

                        result.Category.Add(category);
                        loopCounter++;
                    }
                }

            }
            catch (Exception e)
            {

            }

            return result;
        }

        public List<Venue> GetAllVenues()
        {
            string sqlCommand = "SELECT name FROM venue ORDER by name;";
            List<Venue> result = new List<Venue>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string nameOfVenue = Convert.ToString(reader["name"]);
                        Venue temp = new Venue(nameOfVenue);
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
