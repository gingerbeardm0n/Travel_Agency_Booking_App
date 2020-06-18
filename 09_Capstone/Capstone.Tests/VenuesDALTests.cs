using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Capstone.DAL;
using System.Collections.Generic;
using System;

namespace Capstone.Tests
{
    [TestClass]
    public class VenuesDALTests : ParentTest
    {
        [TestMethod]
        public void GetSpecificVenueTest()
        {
            //Arrange
            VenuesDAL testObj = new VenuesDAL(connectionString);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string sqlInsertVenue = "INSERT INTO venue (name, city_id, description) VALUES ('ZZZZ', 3, 'XXXX')";
                string sqlGetInsertedVenueID = "SELECT id FROM venue WHERE name = 'ZZZZ'";
                string sqlInsertCategories ="INSERT INTO category_venue (venue_id, category_id) VALUES (@id, 1)"
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
            //Act
            Venue temp = testObj.GetSpecificVenue("Hidden Owl Eatery");
            //Assert
            Assert.AreEqual(2, temp.Category.Count);

        }

        [TestMethod]
        public void GetAllVenuesTest()
        {
            //Arrange
            VenuesDAL testObj = new VenuesDAL(connectionString);
            //Act
            List<Venue> temp = testObj.GetAllVenues();
            //Assert
            Assert.IsTrue(temp.Count > 0);

        }
    }
}
