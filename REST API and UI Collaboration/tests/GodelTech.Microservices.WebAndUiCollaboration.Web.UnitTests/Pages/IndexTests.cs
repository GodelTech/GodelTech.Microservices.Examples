using Xunit;
using GodelTech.Microservices.WebAndApiCollaboration.Web.Pages;

namespace GodelTech.Microservices.WebAndApiCollaboration.Web.UnitTests.Pages
{
    public class IndexTests
    {
        [Fact]
        public void WhenOnGet_ThenExceptionsIsNotThrown()
        {
            // Arrange
            var indexModel = new IndexModel();

            // Assert
            indexModel.OnGet();
        }
    }
}