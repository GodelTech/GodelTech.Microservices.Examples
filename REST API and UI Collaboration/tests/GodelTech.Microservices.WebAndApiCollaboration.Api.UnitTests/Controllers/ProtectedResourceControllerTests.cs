using GodelTech.Microservices.WebAndApiCollaboration.Api.Controllers;
using Xunit;

namespace GodelTech.Microservices.WebAndApiCollaboration.Api.UnitTests.Controllers
{
    public class ProtectedResourceControllerTests
    {
        [Fact]
        public void WhenGet_ThenValidStringReturns()
        {
            // Arrange
            const string expectedString = "Protected resource";
            var protectedResourceController = new ProtectedResourceController();

            // Act
            var result = protectedResourceController.Get();

            // Assert
            Assert.Equal(expectedString, result);
        }
    }
}
