using GodelTech.Microservices.Core;
using Microservice.Crm.v1.Resources.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Crm.v1
{
    public class RestApiInitializer : MicroserviceInitializerBase
    {
        public RestApiInitializer(IConfiguration configuration) 
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICreateCommand, CreateCommand>();
            services.AddTransient<IGetCommand, IGetCommand>();
        }
    }
}
