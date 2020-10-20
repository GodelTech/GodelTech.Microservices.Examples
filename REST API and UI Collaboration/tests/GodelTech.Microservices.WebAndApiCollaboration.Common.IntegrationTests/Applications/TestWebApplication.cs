using System;
using System.Net.Http;
using GodelTech.Microservices.WebAndApiCollaboration.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GodelTech.Microservices.WebAndApiCollaboration.Common.IntegrationTests.Applications
{
    public class TestWebApplication
    {
        public const string WebDomain = "https://localhost:44303";
        private readonly IHost _host;

        public TestWebApplication()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseEnvironment("int");
                    webBuilder.UseUrls(WebDomain);
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appSettings.web.int.json", true, true);
                })
                .Build();
        }

        public HttpClient BuildClient(HttpClientHandler handler)
        {
            return new HttpClient(handler)
            {
                BaseAddress = new Uri(WebDomain),
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
