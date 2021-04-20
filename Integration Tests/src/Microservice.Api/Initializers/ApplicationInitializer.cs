using System;
using GodelTech.Microservices.Core;
using Microservice.Application;
using Microservice.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Api.Initializers
{
    public class ApplicationInitializer : MicroserviceInitializerBase
    {
        public ApplicationInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var baseUrl = new Uri(Configuration.GetSection("AgifyEndpoint:BaseAddress").Value);

            services.AddHttpClient(NicknameService.HttpClientName, client =>
            {
                client.BaseAddress = baseUrl;
            });

            services.AddTransient<IAddNicknamesCommand, AddNicknamesCommand>();
            services.AddTransient<IGetTheYoungestNicknameCommand, GetTheYoungestNicknameCommand>();
            services.AddTransient<INicknameService, NicknameService>();
        }
    }
}
