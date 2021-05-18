using System;
using System.Reflection;
using Microservice.Api;
using Microservice.DataAccessEFCore;
using Microservice.DataAccessEFCore.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.SubSystemTests
{
    public sealed class StartUpFixture : IDisposable
    {
        private readonly IWebHost _webHost;

        public StartUpFixture()
        {
            ConfigureStoryLine();

            _webHost = ConfigureWebHost();

            AddInitialStateForDatabase();
        }

        public void ClearNicknamesInDatabase()
        {
            var context = _webHost.Services.CreateScope().ServiceProvider.GetService<NicknamesContext>();

            foreach (var entity in context.Nicknames)
            {
                context.Nicknames.Remove(entity);
            }

            context.SaveChanges();
        }

        private IWebHost ConfigureWebHost()
        {
            return WebHost.CreateDefaultBuilder(null)
                .UseStartup<IntegrationStartup>()
                .UseSolutionRelativeContentRoot("test/Microservice.SubSystemTests")
                .UseSetting("http_port", "5000")
                .ConfigureTestServices(services =>
                {
                    services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
                })
                .ConfigureAppConfiguration((config) =>
                    config.AddJsonFile("appsettings.int.json", false, false))
                .Start();
        }

        private void ConfigureStoryLine()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.int.json", false, false)
                .Build();

            GodelTech.StoryLine.Rest.Config.AddServiceEndpont(
                "GodelTech.Microservices.IntegrationTests.Microservice.Api",
                config["ServiceAddress"]);

            GodelTech.StoryLine.Rest.Config.SetAssemblies(typeof(StartUpFixture).GetTypeInfo().Assembly);

            GodelTech.StoryLine.Wiremock.Config.SetBaseAddress(config["WireMockAddress"]);
        }

        private void AddInitialStateForDatabase()
        {
            var context = _webHost.Services.CreateScope().ServiceProvider.GetService<NicknamesContext>();

            context.Users.Add(new User
            {
                Id = 1,
                Name = "User"
            });

            context.SaveChanges();
        }

        public void Dispose()
        {
            _webHost?.Dispose();
        }
    }
}