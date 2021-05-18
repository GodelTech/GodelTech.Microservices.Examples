using System;
using GodelTech.Microservices.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.SubSystemTests.Initializers
{
    public class InMemoryDatabaseInitializer<TDatabaseContext> : MicroserviceInitializerBase
        where TDatabaseContext : DbContext
    {
        private readonly string _inMemoryDatabaseName;

        public InMemoryDatabaseInitializer(IConfiguration configuration)
            : base(configuration)
        {
            _inMemoryDatabaseName = Guid.NewGuid().ToString();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TDatabaseContext>((p, options) =>
            {
                options.UseInMemoryDatabase(_inMemoryDatabaseName);
            });
        }
    }
}