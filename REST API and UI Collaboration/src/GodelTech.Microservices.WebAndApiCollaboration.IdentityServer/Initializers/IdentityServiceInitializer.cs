using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GodelTech.Microservices.Core;

namespace GodelTech.Microservices.WebAndApiCollaboration.IdentityServer.Initializers
{
    public class IdentityServiceInitializer : MicroserviceInitializerBase
    {
        public IdentityServiceInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));
            if (env == null)
                throw new ArgumentNullException(nameof(env));

            app.UseIdentityServer();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var clients = new List<Client>();
            var apiScopes = new List<ApiScope>();
            var identityResources = new List<IdentityResource>();
            Configuration.Bind("Clients", clients);
            Configuration.Bind("ApiScopes", apiScopes);
            Configuration.Bind("IdentityResources", identityResources);

            services.AddIdentityServer(x =>
                {
                    x.IssuerUri = "http://godeltech";
                })
                .AddInMemoryIdentityResources(identityResources)
                .AddInMemoryApiScopes(apiScopes)
                .AddInMemoryClients(clients)
                .AddDeveloperSigningCredential();
        }
    }
}
