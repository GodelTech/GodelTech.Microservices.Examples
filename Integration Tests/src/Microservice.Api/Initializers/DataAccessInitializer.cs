using GodelTech.Microservices.Core;
using Microservice.DataAccess.Interfaces;
using Microservice.DataAccessEFCore.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Api.Initializers
{
    public class DataAccessInitializer : MicroserviceInitializerBase
    {
        public DataAccessInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserExists, UserExists>();
            services.AddTransient<IAddNicknames, AddNicknames>();
            services.AddTransient<IGetTheYoungestNickname, GetTheYoungestNickname>();
        }
    }
}
