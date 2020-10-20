using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GodelTech.Microservices.WebAndApiCollaboration.Api.IntegrationTests
{
    public class ApiIntegrationsTests : IClassFixture<CustomApiApplicationFactory>
    {
        private readonly CustomApiApplicationFactory _fixture;

        public ApiIntegrationsTests(CustomApiApplicationFactory fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task WhenGetProtectedResource_AndAuthorizedClient_ThenProtectedResourceIsAvailable()
        {
            var response = await _fixture.Client.SendAsync(await _fixture.BuildGetAuthorizedMessage("ProtectedResource")); 
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task WhenGetProtectedResource_AndNotAuthorizedClient_ThenProtectedResourceIsNotAvailable()
        {
            var unauthorizedMessage = new HttpRequestMessage(HttpMethod.Get, "ProtectedResource");
            var response = await _fixture.Client.SendAsync(unauthorizedMessage);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
