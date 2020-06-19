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
        public void TestForInsertionToDataBase()
        {
            //----- Arrange -----------------------------------------------

            ReservationsDAL testObj = new ReservationsDAL(connectionString);
            Reservation dummyReservation = new Reservation(4, "My Test Venue Name", "My Testy Space Name", "JoEli ColinSall", 2, new DateTime(2020,1,1), 1, 500);
            
            //----- Act ---------------------------------------------------

            Reservation testReservation = testObj.AddReservationToDataBase(dummyReservation);

            //----- Assert ------------------------------------------------

            Assert.AreEqual(45, testReservation.ReservationID);
        }



    }
}


