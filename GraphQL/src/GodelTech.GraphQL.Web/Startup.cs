using GodelTech.GraphQL.BL.DI;
using GodelTech.GraphQL.Web.GraphQL;
using GodelTech.GraphQL.Web.GraphQL.GraphType.Property;
using GodelTech.GraphQL.Web.GraphQL.GraphType.PropertyNote;
using GodelTech.GraphQL.Web.Validators;
using GraphQL;
using GraphQL.FluentValidation;
using GraphQL.NewtonsoftJson;
using GraphQL.Server.Ui.Altair;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using Schema = GodelTech.GraphQL.Web.GraphQL.Schema;

namespace GodelTech.GraphQL.Web
{
    public class Startup
    {
        private const string AllowedHosts = "allowedHosts";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    AllowedHosts,
                    builder =>
                    {
                        builder.WithOrigins(Configuration.GetValue<string>("AllowedHosts").Split(","))
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            
            services.AddMvc().AddNewtonsoftJson();

            services.AddHttpContextAccessor();
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console(new RenderedCompactJsonFormatter())
                .CreateLogger();
            
            services.AddBusinessComponents();
            services.AddControllers().AddNewtonsoftJson();
            
            SetUpGraphQL(services);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllowedHosts);
            
            app.UseGraphQLAltair(new GraphQLAltairOptions
            {
                GraphQLEndPoint = Configuration.GetValue<string>("GraphQLSettings:ApiEndpoint"),
                Path = "/GraphQL",
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/health", async context =>
                {
                    await context.Response.WriteAsync("I'm healthy");
                });
            });
        }

        private void SetUpGraphQL(IServiceCollection services)
        {
            // setup taken from https://github.com/GraphQL-dotnet/examples/blob/master/src/AspNetCore/Example/Startup.cs#L15
            
            services.AddTransient(sc =>
            {
                var validatorTypeCache = new ValidatorTypeCache(true);
                validatorTypeCache.AddValidatorsFromAssembly(typeof(Startup).Assembly);

                return validatorTypeCache;
            });
            
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<PropertiesQuery>();
            services.AddSingleton<PropertiesMutation>();

            services.AddSingleton<CreateOrUpdatePropertyNoteInputType>();
            services.AddSingleton<PriceChangeType>();
            services.AddSingleton<PropertyType>();
            
            services.AddSingleton<ISchema, Schema>();
            
            services.AddSingleton<CreateOrUpdatePropertyNoteInputValidator>();
        }
    }
}