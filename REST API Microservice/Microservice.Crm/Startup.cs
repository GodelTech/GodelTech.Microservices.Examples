using System.Collections.Generic;
using GodelTech.Microservices.Core;
using GodelTech.Microservices.Core.HealthChecks;
using GodelTech.Microservices.Core.Mvc;
using GodelTech.Microservices.Core.Services;
using GodelTech.Microservices.EntityFrameworkCore;
using GodelTech.Microservices.Http;
using GodelTech.Microservices.SharedServices;
using GodelTech.Microservices.Swagger;
using GodelTech.Microservices.Swagger.Configuration;
using Microservice.Crm.DataLayer;
using Microservice.Crm.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Microservice.Crm
{
    public sealed class Startup : MicroserviceStartup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IEnumerable<IMicroserviceInitializer> CreateInitializers()
        {
            yield return new CommonServicesInitializer(Configuration);
            yield return new SharedServicesInitializer(Configuration);

            yield return new ServiceClientInitializer(Configuration);
            yield return new CommonMiddlewareInitializer(Configuration);

            yield return new GenericInitializer((app, env) => app.UseRouting());
            yield return new HealthCheckInitializer(Configuration);
            yield return new ApiInitializer(Configuration);

            yield return new SwaggerInitializer(Configuration)
            {
                Options =
                {
                    AuthorizeEndpointUrl = "http://authorize.url",
                    TokenEndpointUrl = "http://token.url",
                    DocumentTitle = "CRM API",
                    SupportedScopes = new[]
                    {
                        new ScopeDetails { Name = "Scope1", Description = "Scope description" }
                    }
                }
            };
            yield return new EntityFrameworkInitializer<CrmDbContext>(Configuration);
            yield return new RestApiInitializer(Configuration);
        }
    }
}