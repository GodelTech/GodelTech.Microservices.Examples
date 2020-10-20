using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GodelTech.Microservices.WebAndApiCollaboration.Common.IntegrationTests.Applications;
using GodelTech.Microservices.WebAndApiCollaboration.Common.IntegrationTests.Services;

namespace GodelTech.Microservices.WebAndApiCollaboration.Api.IntegrationTests
{
    public class CustomApiApplicationFactory : IDisposable
    {
        private readonly TestIdentityServerApplication _identityProviderApp;
        private readonly TestApiApplication _apiApplication;

        public CustomApiApplicationFactory()
        {
            _identityProviderApp = new TestIdentityServerApplication();
            _identityProviderApp.Start();

            _apiApplication = new TestApiApplication();
            _apiApplication.Start();

            Client =_apiApplication.BuildClient();
        }

        public HttpClient Client { get; }

        public async Task<HttpRequestMessage> BuildGetAuthorizedMessage(string url)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, url);
            var token = await GetAccessToken();
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return message;
        }

        private async Task<string> GetAccessToken()
        {
            var service = new TokenService(TestIdentityServerApplication.IdentityServerDomain);

            return await service.GetClientCredentialsTokenAsync("test", "secret", "api1");
        }

        public void Dispose()
        {
            Client?.Dispose();
            _identityProviderApp.Stop();
            _apiApplication.Stop();
        }
    }
}
