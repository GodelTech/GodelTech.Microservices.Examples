using System;
using System.Collections.Generic;
using System.Linq;
using GodelTech.Microservices.Core;

namespace Microservice.IntegrationTests.Extensions
{
    public static class InitializerExtensions
    {
        public static IEnumerable<IMicroserviceInitializer> OverrideInitializer(
            this IEnumerable<IMicroserviceInitializer> initializers,
            Type typeOfOverridenInitializer,
            IMicroserviceInitializer newInitializer)
        {
            var overridenInitializers = initializers.ToArray();

            var initializerIndex =
                Array.FindIndex(overridenInitializers, x => x.GetType() == typeOfOverridenInitializer);

            overridenInitializers[initializerIndex] = newInitializer;

            return overridenInitializers;
        }
    }
}
