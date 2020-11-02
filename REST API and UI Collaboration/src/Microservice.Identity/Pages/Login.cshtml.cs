using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace Microservices.IdentityServer.Pages
{
    public class LoginModel : PageModel
    {
        private readonly TestUser _defaultTestUser = new TestUser
        {
            Claims = new List<Claim>
            {
                new Claim("sub", "alice")
            },
            SubjectId = "1"
        };

        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;

        public LoginModel(
            IIdentityServerInteractionService interaction,
            IEventService events)
        {
            _interaction = interaction;
            _events = events;
        }

        [BindProperty]
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            var context = await _interaction.GetAuthorizationContextAsync(ReturnUrl);
            
            await _events.RaiseAsync(new UserLoginSuccessEvent(_defaultTestUser.Username, _defaultTestUser.SubjectId, _defaultTestUser.Username, clientId: context?.Client.ClientId));

            var user = new IdentityServerUser(_defaultTestUser.SubjectId);
            
            await HttpContext.SignInAsync(user);
            
            return Redirect(ReturnUrl);
        }

        public async Task<IActionResult> OnPostCancelAsync()
        {
            var context = await _interaction.GetAuthorizationContextAsync(ReturnUrl);
           
            await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

            var homePage = context.Client.Properties["HomePage"];
            
            return Redirect(homePage);
        }
    }
}
