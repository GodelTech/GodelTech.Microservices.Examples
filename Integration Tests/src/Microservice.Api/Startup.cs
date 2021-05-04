using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using GodelTech.Microservices.Core;
using GodelTech.Microservices.Core.Mvc;
using GodelTech.Microservices.EntityFrameworkCore;
using Microservice.Api.Initializers;
using Microservice.DataAccessEFCore;
using Microsoft.Extensions.Configuration;

namespace Microservice.Api
{
    public class Startup : MicroserviceStartup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IEnumerable<IMicroserviceInitializer> CreateInitializers()
        {
            yield return new MissingDependenciesInitializer(Configuration);
            yield return new ErrorHandlingInitializer(Configuration);
            yield return new CommonMiddlewareInitializer(Configuration);
            yield return new EntityFrameworkInitializer<NicknamesContext>(Configuration);
            yield return new GenericInitializer((app, env) => app.UseRouting());
            yield return new DataAccessInitializer(Configuration);
            yield return new ApplicationInitializer(Configuration);
            yield return new ValidationInitializer(Configuration);
            yield return new ApiInitializer(Configuration);
        }
    }
}
