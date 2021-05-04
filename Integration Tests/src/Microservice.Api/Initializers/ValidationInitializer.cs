using FluentValidation;
using GodelTech.Microservices.Core;
using Microservice.Api.Models;
using Microservice.Api.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Api.Initializers
{
    public class ValidationInitializer : MicroserviceInitializerBase
    {
        public ValidationInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IValidator<UserNicknames>, UserNicknamesValidator>();
        }
    }
}
