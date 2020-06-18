using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationsDALTests : ParentTest
    {
        [TestMethod]
        public void TestIfInsertionToDataBase()
        {
            //----- Arrange -----------------------------------------------

            ReservationsDAL testObj = new ReservationsDAL(connectionString);
            Reservation dummyReservation = new Reservation(4, "My Test Venue Name", "My Testy Space Name", "JoEli ColinSall", 2, 2050-01-01, 1, 500);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlInsertReservation



                string sqlInsertVenue = "INSERT INTO venue (name, city_id, description) VALUES ('ZZZZ', 3, 'XXXX')";
                string sqlGetInsertedVenueID = "SELECT id FROM venue WHERE name = 'ZZZZ'";
                string sqlInsertCategories = "INSERT INTO category_venue (venue_id, category_id) VALUES (@id, 1)"
                + "INSERT INTO category_venue (venue_id, category_id) VALUES (@id, 2);";

                SqlCommand cmd = new SqlCommand(sqlInsertVenue, conn);
                int count = cmd.ExecuteNonQuery();

                cmd = new SqlCommand(sqlGetInsertedVenueID, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                int newId = 0;
                while (reader.Read())
                {
                    newId = Convert.ToInt32(reader["id"]);
                }
                reader.Close();
                cmd = new SqlCommand(sqlInsertCategories, conn);
                cmd.Parameters.AddWithValue("@id", newId);
                cmd.ExecuteNonQuery();


            }



                //----- Act ---------------------------------------------------

                testObj.AddReservationToDataBase(reservation);


            //----- Assert ------------------------------------------------


        }



    }
}


