using System;
using System.Collections.Generic;
using GodelTech.Microservices.Core;
using GodelTech.Microservices.EntityFrameworkCore;
using Microservice.IntegrationTests.Extensions;
using Microservice.IntegrationTests.Initializers;
using Microservice.Api;
using Microservice.DataAccessEFCore;
using Microsoft.Extensions.Configuration;

namespace Microservice.IntegrationTests
{
    public class IntegrationStartup : Startup
    {
        public IntegrationStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IEnumerable<IMicroserviceInitializer> CreateInitializers()
        {
            var baseInitializer = base.CreateInitializers();

            return baseInitializer.OverrideInitializer(
                typeof(EntityFrameworkInitializer<NicknamesContext>),
                new InMemoryDatabaseInitializer<NicknamesContext>(Configuration));
        }
    }
}
