using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using GodelTech.Microservices.WebAndApiCollaboration.IdentityServer;

namespace GodelTech.Microservices.WebAndApiCollaboration.Common.IntegrationTests.Applications
{
    public class TestIdentityServerApplication
    {
        public const string IdentityServerDomain = "https://localhost:44301";
        private readonly IHost _host;

        public TestIdentityServerApplication()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseEnvironment("int");
                    webBuilder.UseUrls(IdentityServerDomain);
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appSettings.is.int.json", true, true);
                })
                .Build();
        }

        public HttpClient BuildClient(HttpClientHandler handler)
        {
            return new HttpClient(handler)
            {
                BaseAddress = new Uri(IdentityServerDomain),
            };
        }

        public void Start()
        {
            _host.StartAsync().GetAwaiter().GetResult();
        }

        public void Stop()
        {
            _host.StopAsync().GetAwaiter().GetResult();
        }
    }
}
