using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GodelTech.Microservices.WebAndApiCollaboration.Web.IntegrationTests
{
    public class WebIntegrationsTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _fixture;

        public WebIntegrationsTests(CustomWebApplicationFactory fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task WhenGetHome_ThenStatusCodeIsOk()
        {
            var response = await _fixture.Client.GetAsync("/"); 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenGetError_ThenStatusCodeIsOk()
        {
            var response = await _fixture.Client.GetAsync("/Error");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenGetUnauthorized_ThenStatusCodeIsOk()
        {
            var response = await _fixture.Client.GetAsync("/Unauthorized");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenGetPrivacy_AndUserIsNotAuthenticated_ThenPageIsUnauthorized()
        {
            var response = await _fixture.Client.GetAsync("/Privacy");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("/Unauthorized", response.RequestMessage.RequestUri.AbsolutePath);
        }

        [Fact]
        public async Task WhenGetAccount_ThenRedirectToIdentityServer()
        {
            var response = await _fixture.Client.GetAsync("/Account");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("/Account/Login", response.RequestMessage.RequestUri.AbsolutePath);
            Assert.Equal("localhost:44301", response.RequestMessage.RequestUri.Authority);
        }
    }
}
