using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using GodelTech.Microservices.WebAndApiCollaboration.Api;

namespace GodelTech.Microservices.WebAndApiCollaboration.Common.IntegrationTests.Applications
{
    public class TestApiApplication
    {
        public const string ApiDomain = "https://localhost:44302";
        private readonly IHost _host;

        public TestApiApplication()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseEnvironment("int");
                    webBuilder.UseUrls(ApiDomain);
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appSettings.api.int.json", true, true);
                })
                .Build();
        }

        public HttpClient BuildClient()
        {
            return new HttpClient()
            {
                BaseAddress = new Uri(ApiDomain)
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
