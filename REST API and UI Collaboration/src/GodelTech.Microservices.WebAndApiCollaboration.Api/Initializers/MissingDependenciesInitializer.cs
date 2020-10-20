using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GodelTech.Microservices.Core;
using GodelTech.Microservices.Core.Services;

namespace GodelTech.Microservices.WebAndApiCollaboration.Api.Initializers
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
        }
    }
}