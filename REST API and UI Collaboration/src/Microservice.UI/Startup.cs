using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using GodelTech.Microservices.Core;
using GodelTech.Microservices.Core.Mvc;
using GodelTech.Microservices.Http;
using GodelTech.Microservices.Security;
using Microservices.Web.Initializers;

namespace Microservices.Web
{
    public class Startup : MicroserviceStartup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IEnumerable<IMicroserviceInitializer> CreateInitializers()
        {
            yield return new CommonMiddlewareInitializer(Configuration);
            yield return new MissingDependenciesInitializer(Configuration);
            yield return new DeveloperExceptionPageInitializer(Configuration)
            {
                ErrorHandlingPath = "/Error"
            };
            yield return new GenericInitializer((app, env) => app.UseStaticFiles());
            yield return new GenericInitializer((app, env) => app.UseRouting());
            yield return new UiSecurityInitializer(Configuration);
            yield return new ServiceClientInitializer(Configuration);
            yield return new AuthorizedRazorPagesInitializer(Configuration);
        }
    }
}
