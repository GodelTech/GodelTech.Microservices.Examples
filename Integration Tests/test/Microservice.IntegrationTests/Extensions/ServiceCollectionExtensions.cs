using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.IntegrationTests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ReplaceWithFake<TService>(this IServiceCollection collection, TService replacement) where TService : class
        {
            var service = collection.FirstOrDefault(x => x.ServiceType == typeof(TService));

            collection.Remove(service);
            collection.AddTransient(_ => replacement);

            return collection;
        }
    }
}
