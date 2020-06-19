using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Capstone.DAL;
using System.Collections.Generic;
using System;

namespace Capstone.Tests
{
    [TestClass]
    public class SpacesDALTests : ParentTest
    {
        [TestMethod]
        public void GetSpacesForVenueTest()
        {
            //Arrange
            SpacesDAL testObj = new SpacesDAL(connectionString);
            int newId = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlInsertVenue = "INSERT INTO venue (name, city_id, description) VALUES ('ZZZZ', 3, 'XXXX')";
                string sqlGetInsertedVenueID = "SELECT id FROM venue WHERE name = 'ZZZZ'";
                string sqlInsertSpaces = "INSERT INTO space (venue_id, name, is_accessible, daily_rate, max_occupancy) VALUES (@id, 'YYYY', 1, 400.00, 100)"
                + "INSERT INTO space (venue_id, name, is_accessible, daily_rate, max_occupancy) VALUES (@id, 'VVVV', 1, 3500.00, 200); ";

                SqlCommand cmd = new SqlCommand(sqlInsertVenue, conn);
                int count = cmd.ExecuteNonQuery();

                cmd = new SqlCommand(sqlGetInsertedVenueID, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    newId = Convert.ToInt32(reader["id"]);
                }
                reader.Close();

                cmd = new SqlCommand(sqlInsertSpaces, conn);
                cmd.Parameters.AddWithValue("@id", newId);
                cmd.ExecuteNonQuery();
            }

            //Act
            List<Space> result = testObj.GetSpacesForVenue(newId);

            //Assert
            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        public void SearchAvailableSpacesTest()
        {
            //Arrange
            SpacesDAL testObj = new SpacesDAL(connectionString);
            DateTime testDate = new DateTime(2020, 6, 15);
            int newId = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlInsertVenue = "INSERT INTO venue (name, city_id, description) VALUES ('ZZZZ', 3, 'XXXX')";
                string sqlInsertSpace = "INSERT INTO space (name, is_accessible, venue_id, daily_rate, max_occupancy, open_from, open_to) VALUES ('RRRR', 0, @venue_id, 350.00, 100000, 2, 10)"
                     + "INSERT INTO space (name, is_accessible, venue_id, daily_rate, max_occupancy) VALUES ('ZZZZ', 1, @venue_id, 600.00, 100000)";
                string sqlGetInsertedVenueID = "SELECT id FROM Venue WHERE name = 'ZZZZ'";

                SqlCommand cmd = new SqlCommand(sqlInsertVenue, conn);
                int count = cmd.ExecuteNonQuery();
                Assert.IsTrue(count > 0);

                cmd = new SqlCommand(sqlGetInsertedVenueID, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    newId = Convert.ToInt32(reader["id"]);
                }
                reader.Close();

                cmd = new SqlCommand(sqlInsertSpace, conn);
                cmd.Parameters.AddWithValue("@venue_id", newId);
                count = cmd.ExecuteNonQuery();
                Assert.IsTrue(count > 0);

            }

            //Act
            List<Space> result = testObj.SearchForAvailableSpaces(newId, testDate, 1, 99999);
            
            //Assert
            Assert.AreEqual(2, result.Count);
        }
    }
}
