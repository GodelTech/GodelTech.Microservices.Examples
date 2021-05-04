using System.Collections.Generic;
using Microservice.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.IntegrationTests
{
    public delegate void ServiceConfiguration(IServiceCollection services);

    public class IntegrationWebApplicationFactory : WebApplicationFactory<IntegrationStartup>
    {
        private readonly ServiceConfiguration _serviceConfiguration;

        public IntegrationWebApplicationFactory(ServiceConfiguration serviceConfiguration = null)
        {
            _serviceConfiguration = serviceConfiguration;
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = WebHost.CreateDefaultBuilder(null);

            builder.ConfigureAppConfiguration(configure =>
            {
                configure.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"AgifyEndpoint:BaseAddress", "https://api.agify.io/"},
                });
            });

            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseStartup<IntegrationStartup>()
                .UseSolutionRelativeContentRoot("test/Microservice.IntegrationTests");

            builder.ConfigureTestServices(services =>
            {
                services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
                _serviceConfiguration?.Invoke(services);
            });
        }

        public TContext GetDbContext<TContext>() where TContext : DbContext => Services.CreateScope().ServiceProvider.GetService<TContext>();
    }
}