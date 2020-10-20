using System;
using System.Net;
using System.Net.Http;
using GodelTech.Microservices.WebAndApiCollaboration.Common.IntegrationTests.Applications;

namespace GodelTech.Microservices.WebAndApiCollaboration.Web.IntegrationTests
{
    public class CustomWebApplicationFactory : IDisposable
    {
        private readonly TestIdentityServerApplication _identityProviderApp;
        private readonly TestApiApplication _apiApplication;
        private readonly TestWebApplication _webApplication;

        public CustomWebApplicationFactory()
        {
            _identityProviderApp = new TestIdentityServerApplication();
            _identityProviderApp.Start();

            _apiApplication = new TestApiApplication();
            _apiApplication.Start();

            _webApplication = new TestWebApplication();
            _webApplication.Start();

            CookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() {CookieContainer = CookieContainer };

            Client = _webApplication.BuildClient(handler);
            IdentityServerClient = _identityProviderApp.BuildClient(handler);
        }

        public HttpClient Client { get; }
        public HttpClient IdentityServerClient { get; }
        public CookieContainer CookieContainer { get; }

        public void Dispose()
        {
            Client?.Dispose();
            _identityProviderApp.Stop();
            _apiApplication.Stop();
            _webApplication.Stop();
        }
    }
}