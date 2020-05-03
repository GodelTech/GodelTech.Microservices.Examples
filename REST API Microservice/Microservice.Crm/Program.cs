using System.Collections.Generic;
using GodelTech.Microservices.Core;
using Microsoft.Extensions.Hosting;

namespace Microservice.Crm
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            MicroserviceHost.CreateHostBuilder<Startup>(args, new Dictionary<string, string>
            {
                ["DB_CONNECTION_STRING"] = "ConnectionStrings:Default",
            });
    }
}
