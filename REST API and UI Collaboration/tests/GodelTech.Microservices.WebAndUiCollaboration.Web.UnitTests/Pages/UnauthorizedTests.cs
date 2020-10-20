using Xunit;
using GodelTech.Microservices.WebAndApiCollaboration.Web.Pages;

namespace GodelTech.Microservices.WebAndApiCollaboration.Web.UnitTests.Pages
{
    public class UnauthorizedTests
    {
        [Fact]
        public void WhenOnGet_ThenExceptionsIsNotThrown()
        {
            // Arrange
            var unauthorizedModel = new UnauthorizedModel();

            // Assert
            unauthorizedModel.OnGet();
        }
    }
}
