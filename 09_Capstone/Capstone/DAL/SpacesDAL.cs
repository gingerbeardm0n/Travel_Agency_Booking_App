using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
namespace Capstone.DAL
{
    public class SpacesDAL
    {
        private string connectionString;
        public SpacesDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Space> GetSpacesForVenue(int venueID)
        {
            string sqlCommandNotNull = "SELECT * FROM space WHERE venue_id = @venue_id;";
            List<Space> result = new List<Space>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlCommandNotNull, conn);
                    cmd.Parameters.AddWithValue("@venue_id", venueID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int spaceID = Convert.ToInt32(reader["id"]);
                        string spaceName = Convert.ToString(reader["name"]);
                        bool spaceAccessible = Convert.ToBoolean(reader["is_accessible"]);

                        int spaceOpenFrom;
                        if (DBNull.Value.Equals(reader["open_from"]))
                        {
                            spaceOpenFrom = 0;
                        }
                        else
                        {
                            spaceOpenFrom = Convert.ToInt32(reader["open_from"]);
                        }

                        int spaceOpenTo;
                        if (DBNull.Value.Equals(reader["open_to"]))
                        {
                            spaceOpenTo = 0;
                        }
                        else
                        {
                            spaceOpenTo = Convert.ToInt32(reader["open_to"]);
                        }

                        Decimal spaceDailyRate = Convert.ToDecimal(reader["daily_rate"]);
                        int spaceMaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);

                        Space temp = new Space(spaceID, spaceName, 
                            spaceDailyRate, spaceMaxOccupancy,
                            spaceAccessible, spaceOpenFrom,
                            spaceOpenTo);
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
