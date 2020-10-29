using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GodelTech.Microservices.Core.Mvc;

namespace Microservices.Web.Initializers
{
    public class AuthorizedRazorPagesInitializer : RazorPagesInitializer
    {
        public AuthorizedRazorPagesInitializer(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureRazorPagesOptions(RazorPagesOptions options)
        {
            options.Conventions.AuthorizePage("/Account");
        }
    }
}
