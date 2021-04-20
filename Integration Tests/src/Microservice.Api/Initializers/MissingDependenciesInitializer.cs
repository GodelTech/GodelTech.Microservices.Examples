using System;
using GodelTech.Microservices.Core;
using GodelTech.Microservices.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Api.Initializers
{
    public class MissingDependenciesInitializer : MicroserviceInitializerBase
    {
        public MissingDependenciesInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ICorrelationIdSetter, CorrelationIdAccessor>();
            services.AddSingleton<ICorrelationIdAccessor, CorrelationIdAccessor>();
        }
    }
}
