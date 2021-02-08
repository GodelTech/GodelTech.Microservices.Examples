using GodelTech.GraphQL.BL.Services;
using GodelTech.GraphQL.BL.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace GodelTech.GraphQL.BL.DI
{
    public static class DependencyInjectionExtensions
    {
        public static void AddBusinessComponents(this IServiceCollection services)
        {
            services.AddTransient<IPropertiesService, PropertiesService>();
        }
    }
}