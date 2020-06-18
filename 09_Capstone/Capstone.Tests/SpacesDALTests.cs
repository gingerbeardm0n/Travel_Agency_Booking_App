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
            //Act
            List<Space> result = testObj.GetSpacesForVenue(1);
            //Assert
            Assert.AreEqual(7, result.Count);
        }
        [TestMethod]
        public void SearchAvailableSpacesTest()
        {
            //Arrange
            SpacesDAL testObj = new SpacesDAL(connectionString);
            DateTime testDate = new DateTime(2020, 6, 15);
            //Act

            List<Space> result = testObj.SearchForAvailableSpaces(2, testDate, 3, 30);
            //Assert
            Assert.AreEqual(3, result.Count);
        }
    }
}
