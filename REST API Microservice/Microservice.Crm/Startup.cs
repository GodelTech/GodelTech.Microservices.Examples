using System.Collections.Generic;
using GodelTech.Microservices.Core;
using GodelTech.Microservices.EntityFrameworkCore;
using GodelTech.Microservices.Swagger;
using Microservice.Crm.DataLayer;
using Microsoft.Extensions.Configuration;

namespace Microservice.Crm
{
    public sealed class Startup : MicroserviceStartup
    {
        public static class Scopes
        {
            public const string ClientsRead = "clients.read";
            public const string ClientsManage = "clients.manage";
        }

        public static class Policies
        {
            public const string ClientsRead = "ClientsReadPolicy";
            public const string ClientsManage = "ClientsManagePolicy";
        }

        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IEnumerable<IMicroserviceInitializer> CreateInitializers()
        {
            foreach (var initializer in base.CreateInitializers())
            {
                yield return initializer;
            }

            yield return new SwaggerInitializer(Configuration);
            yield return new EntityFrameworkInitializer<CrmDbContext>(Configuration);
        }
    }
}