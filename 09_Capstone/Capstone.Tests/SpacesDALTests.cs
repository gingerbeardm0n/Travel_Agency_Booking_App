using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Capstone.DAL;
using System.Collections.Generic;

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
    }
}
