using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Capstone.DAL;
using System.Collections.Generic;
using System;

namespace Capstone.Tests
{
    [TestClass]
    public class BookingSystemTests : ParentTest
    {
        [TestMethod]
        public void MakeReservationTest()
        {
            //Arrange
            BookingSystem testObj = new BookingSystem(connectionString);
            Reservation result;
            int newId = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlInsertVenue = "INSERT INTO space (name, is_accessible, venue_id, daily_rate, max_occupancy) VALUES ('ZZZZ', 1, 1, 600.00, 150)";
                string sqlGetInsertedVenueID = "SELECT id FROM space WHERE name = 'ZZZZ'";

                SqlCommand cmd = new SqlCommand(sqlInsertVenue, conn);
                int count = cmd.ExecuteNonQuery();

                cmd = new SqlCommand(sqlGetInsertedVenueID, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    newId = Convert.ToInt32(reader["id"]);
                }
                reader.Close();

            }

            List<Space> spaces = new List<Space>();
            Space temp = new Space(newId, "ZZZZ", 600M, 150, true);
            spaces.Add(temp);
            DateTime tempDate = new DateTime(2020, 1, 1);

            //Act
            result = testObj.MakeReservation(spaces, newId, 100, tempDate, 1, "Hidden Owl Eatery", "TestRes");

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ReservationID);
        }

    }
}
