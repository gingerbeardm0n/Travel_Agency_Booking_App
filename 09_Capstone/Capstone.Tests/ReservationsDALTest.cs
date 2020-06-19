using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Tests;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationsDALTests : ParentTest
    {
        [TestMethod]
        public void TestForInsertionToDataBase()
        {
            //----- Arrange -----------------------------------------------

            ReservationsDAL testObj = new ReservationsDAL(connectionString);
            Reservation dummyReservation = new Reservation(4, "My Test Venue Name", "My Testy Space Name", "JoEli ColinSall", 2, new DateTime(2020,1,1), 1, 500);
            
            //----- Act ---------------------------------------------------

            int startingRowCount = GetRowCount("reservation");
            Reservation testReservation = testObj.AddReservationToDataBase(dummyReservation);
            int endingRowCount = GetRowCount("city");

            //----- Assert ------------------------------------------------

            Assert.AreNotEqual(startingRowCount, endingRowCount);
        }
        protected int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}


