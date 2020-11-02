using System.Diagnostics;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Microservices.IdentityServer.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly ILogger<ErrorModel> _logger;

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrWhiteSpace(RequestId);

        public ErrorModel(
            IIdentityServerInteractionService identityServerInteractionService,
            ILogger<ErrorModel> logger)
        {
            _identityServerInteractionService = identityServerInteractionService;
            _logger = logger;
        }

        public async Task OnGet(string errorId)
        {
            var error = await _identityServerInteractionService.GetErrorContextAsync(errorId);
            
            _logger.LogError($"Client id: {error.ClientId}. Error: {error.Error}. Error description: {error.ErrorDescription}");

            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
