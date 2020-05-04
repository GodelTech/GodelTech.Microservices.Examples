using System.Collections.Generic;
using Autofac.Extensions.DependencyInjection;
using GodelTech.Microservices.Core.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Microservice.Crm
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging((context, logging) =>
                {
                    // Lines below are required to exclude default .NET Core logging providers
                    // These providers are excluded to make sure that logging format remains
                    // the same as in .NET Framework microservices.
                    logging.ClearProviders();
                })
                .UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
                })
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddInMemoryCollectionFromEnvVariables(new Dictionary<string, string>
                    {
                        ["DB_CONNECTION_STRING"] = "ConnectionStrings:Default",
                    });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
