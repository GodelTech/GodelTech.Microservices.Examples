using System.Linq;
using Xunit;
using GodelTech.Microservices.LambdaFunctionApp.Controllers;

namespace GodelTech.Microservices.LambdaFunctionApp.Unit.Tests.Controllers
{
    public class ValuesControllerTests
    {
        private readonly ValuesController _valuesController;

        public ValuesControllerTests()
        {
            _valuesController = new ValuesController();
        }

        [Fact]
        public void WhenGet_ValuesAreReturned()
        {
            // Act
            var result = _valuesController.Get().ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("value1", result[0]);
            Assert.Equal("value2", result[1]);
        }

        [Fact]
        public void WhenGet_AndIdIsProvided_ThenSpecificValueIsReturned()
        {
            // Act
            var result = _valuesController.Get(1).ToList();

            // Assert
            Assert.Equal("value", result);
        }
    }
}
