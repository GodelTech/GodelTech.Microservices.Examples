using System.Collections.Generic;
using GodelTech.Microservices.Core;
using GodelTech.Microservices.Core.Subsys;
using Microsoft.Extensions.Configuration;

namespace Microservice.Crm
{
    public sealed class Startup : MicroserviceStartup
    {
        private const string ServiceName = "a";
        private const string ReadIntent = "r";
        private const string ManageIntent = "m";

        public static class Scopes
        {
            public const string JobsRead = ServiceName + ".jobs." + ReadIntent;
            public const string JobsManage = ServiceName + ".jobs." + ManageIntent;
        }

        public static class Policies
        {
            public const string JobsRead = "JobsReadPolicy";
            public const string JobsManage = "JobsManagePolicy";
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

            //yield return new EntityFrameworkInitializer<AnalyzerDbContext>(Configuration);
        }
    }
}