using
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Models;
using Telstar_Logistics_Parcel_Delivery_Solution.Controllers;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;

namespace ControllerTest
{
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "mydatabase_" + DateTime.Now.ToFileTimeUtc());

            _dbContextMock = new Mock<ApplicationDbContext>(optionsBuilder.Options);
            _dbContextMock.When(applicationDbContext.Categories.ToList()).ThenReturn(new List<String>() { Fragile, Cooled });
        }

        [Test]
        public void Happy_CategoryController_Test()
        {
            // Arrange
            var controller = new CategoryController(_dbContextMock);
            List<String> expectedResponse = new List<String>() { Fragile, Cooled };

            // Act
            var result = controller.GetExtCities();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedResponse.FindIndex(0), result.FindIndex(0));
            Assert.AreEqual(expectedResponse.FindIndex(1), result.FindIndex(1));

        }

        [Test]
        public void Sad_CategoryController_Test()
        {
            // Arrange
            var controller = new CategoryController(_dbContextMock);
            List<String> expectedResponse = new List<String>() { Kossa, Elefant };

            // Act
            var result = controller.GetExtCities();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreNotEqual(expectedResponse.FindIndex(0), result.FindIndex(0));
            Assert.AreNotEqual(expectedResponse.FindIndex(1), result.FindIndex(1));

        }

    }
}