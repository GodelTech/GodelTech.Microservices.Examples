using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityServer4.Events;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;
using GodelTech.Microservices.WebAndApiCollaboration.IdentityServer.Pages;

namespace GodelTech.Microservices.WebAndApiCollaboration.IdentityServer.UnitTests.Pages
{
    public class LogoutTests
    {
        private const string LogoutId = "LogoutId";
        private const string ReturnUrl = "ReturnUrl";

        private readonly Mock<IEventService> _eventService;
        private readonly HttpContext _httpContext;
        private readonly Mock<IAuthenticationService> _authenticationService;
        private readonly LogoutModel _testSubject;

        public LogoutTests()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var identity = new ClaimsIdentity(
                Mock.Of<IIdentity>(),
                new List<Claim>{new Claim("sub", "1")},
                "Cookies",
                null,
                null);

            var principal = new ClaimsPrincipal(identity);

            _authenticationService = new Mock<IAuthenticationService>();
            var identityServerInteractionService = new Mock<IIdentityServerInteractionService>();
            _eventService = new Mock<IEventService>();

            _httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider.Object,
                User = principal
            };

            identityServerInteractionService
                .Setup(x => x.GetLogoutContextAsync(LogoutId))
                .ReturnsAsync(new LogoutRequest(default, default) { PostLogoutRedirectUri = ReturnUrl });
            serviceProvider.Setup(x => x.GetService(typeof(IAuthenticationService)))
                .Returns(_authenticationService.Object);
            _testSubject = new LogoutModel(identityServerInteractionService.Object, _eventService.Object)
            {
                PageContext = new PageContext
                {
                    HttpContext = _httpContext
                }
            };
        }

        [Fact]
        public async Task WhenOnGet_ThenLogoutIdIsSuccessful()
        {
            // Act
            await _testSubject.OnGet(LogoutId);

            // Assert
            _authenticationService
                .Verify(
                    x => x.SignOutAsync(
                        _httpContext,
                        null,
                        null),
                    Times.Once);
        }

        [Fact]
        public async Task WhenOnPostAsync_AndUserIsAuthenticated_ThenSignOutIsCalled()
        {
            // Act
            await _testSubject.OnPostLogoutAsync(LogoutId);

            // Assert
            _authenticationService
                .Verify(
                    x => x.SignOutAsync(
                        _httpContext,
                        null,
                         null),
                    Times.Once);
        }

        [Fact]
        public async Task WhenOnPostAsync_AndUserIsAuthenticated_ThenUserLoginSuccessEvent()
        {
            // Act
            await _testSubject.OnPostLogoutAsync(LogoutId);

            // Assert
            _eventService.Verify(
                    x => x.RaiseAsync(It.Is<UserLogoutSuccessEvent>(
                        y => y.DisplayName.Equals("1")
                             && y.SubjectId.Equals("1"))),
                    Times.Once);
        }

        [Fact]
        public async Task WhenOnPostAsync_ThenRedirectionIsValid()
        {
            // Act
            var result = await _testSubject.OnPostLogoutAsync(LogoutId);

            // Assert
            Assert.True(result is RedirectResult);
            Assert.Equal(ReturnUrl, ((RedirectResult)result).Url);
        }
    }
}
