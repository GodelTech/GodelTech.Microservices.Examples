using System.Threading.Tasks;
using Moq;
using Xunit;
using GodelTech.Microservices.Http.Services;
using GodelTech.Microservices.WebAndApiCollaboration.Web.Pages;

namespace GodelTech.Microservices.WebAndApiCollaboration.Web.UnitTests.Pages
{
    public class PrivacyTests
    {
        private readonly Mock<IServiceClient> _serviceClient;

        private readonly PrivacyModel _testSubject;

        public PrivacyTests()
        {
            _serviceClient = new Mock<IServiceClient>();
            var serviceClientFactory = new Mock<IServiceClientFactory>();
            serviceClientFactory
                .Setup(x => x.Create("api1", false))
                .Returns(_serviceClient.Object);

            _testSubject = new PrivacyModel(serviceClientFactory.Object);
        }

        [Fact]
        public async Task WhenOnGet_ThenThenProtectedResourceIsValid()
        {
            // Arrange
            const string ProtectedResource = "ProtectedResource";
            _serviceClient
                .Setup(x => x.GetAsync<string>("ProtectedResource"))
                .ReturnsAsync(ProtectedResource);

            // Act
            await _testSubject.OnGet();

            // Assert
            Assert.Equal(ProtectedResource, _testSubject.ProtectedResource);
        }
    }
}
