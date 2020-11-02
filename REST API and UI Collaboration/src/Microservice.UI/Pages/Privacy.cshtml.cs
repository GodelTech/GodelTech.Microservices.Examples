using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GodelTech.Microservices.Http.Services;

namespace Microservices.Web.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly IServiceClientFactory _serviceClientFactory;

        [BindProperty]
        public string ProtectedResource { get; set; }

        public PrivacyModel(IServiceClientFactory serviceClientFactory)
        {
            _serviceClientFactory = serviceClientFactory;
        }

        public async Task OnGet()
        {
            var client = _serviceClientFactory.Create("api1");
            
            ProtectedResource = await client.GetAsync<string>("ProtectedResource");
        }
    }
}

