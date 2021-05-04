using GodelTech.Microservices.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microservice.Api.Middleware;

namespace Microservice.Api.Initializers
{
    public class ErrorHandlingInitializer : MicroserviceInitializerBase
    {
        public ErrorHandlingInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
