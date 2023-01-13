using Microsoft.EntityFrameworkCore;
using Models;
using Telstar_Logistics_Parcel_Delivery_Solution.Controllers;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace ControllerTest
{
    public class Tests
    {
        [SetUp] public void SetUp() {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "mydatabase_" + DateTime.Now.ToFileTimeUtc());

            _dbContextMock = new Mock<ApplicationDbContext>(optionsBuilder.Options);
            _dbContextMock.When(applicationDbContext.Categories.ToList()).ThenReturn(new List<String>() { Fragile, Cooled });
            _dbContextMock.When(applicationDbContext.CITY.ToList()).ThenReturn(new List<String>() { Tripoli, Tunis });
        }

        [Test]
        public void Happy_OrderInformationController_Test()
        {
            // Arrange
            var controller = new OrderInformationController(_dbContextMock);
            ExternalOrderRequestDto request = createRequestDto();
            ExternalOrderResponseDto expectedResponse = createResponseDto();

            // Act
            ExternalOrderResponseDto result = controller.Post(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedResponse.cost, result.cost);
            Assert.AreEqual(expectedResponse.duration, result.duration);

        }

        [Test]
        public void Sad_OrderInformationController_Test()
        {
            // Arrange
            var controller = new OrderInformationController(_dbContextMock);
            ExternalOrderRequestDto request = createRequestDto();
            ExternalOrderResponseDto expectedResponse = createSadRequestDto();

            // Act
            ExternalOrderResponseDto result = controller.Post(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreNotEqual(expectedResponse.cost, result.cost);
            Assert.AreNotEqual(expectedResponse.duration, result.duration);
        }

            private ExternalOrderResponseDto createResponseDto()
            {
            ExternalOrderResponseDto response = new ExternalOrderResponseDto("4", "4");
            return response;
            }



            private ExternalOrderRequestDto createRequestDto()
        {
            DimensionsDto dimensions = new DimensionsDto("20", "20", "20");
            List<String> list_categories = new List<String>();
            list_categories.Add("Fragile");
            list_categories.Add("Cooled");
            ExternalOrderRequestDto externalOrderRequestDto = new ExternalOrderRequestDto("Tripoli", "Tunis", "2022-01-01", dimensions, "40", list_categories);
            return externalOrderRequestDto;
        }

            private ExternalOrderRequestDto createSadRequestDto()
            {
                DimensionsDto dimensions = new DimensionsDto("20", "20", "20");
                List<String> list_categories = new List<String>();
                list_categories.Add("Fragile");
                list_categories.Add("Cooled");
                ExternalOrderRequestDto externalOrderRequestDto = new ExternalOrderRequestDto("Malmö", "Trondheim", "2022-01-01", dimensions, "40", list_categories);
                return externalOrderRequestDto;
            }

        }
}