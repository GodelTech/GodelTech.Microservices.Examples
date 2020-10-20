using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Configuration;
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
    public class LoginTests
    {
        private const string ReturnUrl = "ReturnUrl";
        private const string AuthenticationScheme = "Cookies";

        private readonly Mock<IIdentityServerInteractionService> _identityServerInteractionService;
        private readonly Mock<IEventService> _eventService;
        private readonly HttpContext _httpContext;
        private readonly Mock<IAuthenticationService> _authenticationService;
        private readonly LoginModel _testSubject;

        public LoginTests()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var identityOptions = new IdentityServerOptions
            {
                Authentication = new IdentityServer4.Configuration.AuthenticationOptions()
                {
                    CookieAuthenticationScheme = AuthenticationScheme
                }
            };
            _identityServerInteractionService = new Mock<IIdentityServerInteractionService>();
            _eventService = new Mock<IEventService>();
            _httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider.Object
            };

            _authenticationService = new Mock<IAuthenticationService>();
            serviceProvider.Setup(x => x.GetService(typeof(IdentityServerOptions)))
                .Returns(identityOptions);
            serviceProvider.Setup(x => x.GetService(typeof(IAuthenticationService)))
                .Returns(_authenticationService.Object);
            

            _testSubject = new LoginModel(_identityServerInteractionService.Object, _eventService.Object)
            {
                PageContext = new PageContext
                {
                    
                    HttpContext = _httpContext
                }
            };
        }

        [Fact]
        public void WhenOnGet_ThenReturnUrlIsValid()
        {
            // Act
            _testSubject.OnGet(ReturnUrl);

            // Assert
            Assert.Equal(ReturnUrl, _testSubject.ReturnUrl);
        }

        [Fact]
        public async Task WhenOnPostAsync_ThenUserIsAuthenticated()
        {
            // Arrange
            _testSubject.ReturnUrl = ReturnUrl;

            // Act
            await _testSubject.OnPostLoginAsync();

            // Assert
            _authenticationService
                .Verify(
                    x => x.SignInAsync(
                        _httpContext,
                        AuthenticationScheme,
                        It.Is<ClaimsPrincipal>(cp => cp.HasClaim(c => c.Type.Equals("sub"))), null),
                    Times.Once);
        }

        [Fact]
        public async Task WhenOnPostAsync_ThenUserLoginSuccessEvent()
        {
            // Arrange
            _testSubject.ReturnUrl = ReturnUrl;
            var authorizationRequest = new AuthorizationRequest
            {
                Client = new Client
                {
                    ClientId = "ClientId"
                }
            };

            _identityServerInteractionService
                .Setup(x => x.GetAuthorizationContextAsync(ReturnUrl))
                .ReturnsAsync(authorizationRequest);

            // Act
            await _testSubject.OnPostLoginAsync();

            // Assert
            _eventService.Verify(
                    x => x.RaiseAsync(It.Is<UserLoginSuccessEvent>( 
                        y => 
                            y.Username == null
                            && y.DisplayName == null
                            && y.ClientId.Equals(authorizationRequest.Client.ClientId, StringComparison.Ordinal)
                            && y.SubjectId.Equals("1"))),
                    Times.Once);
        }

        [Fact]
        public async Task WhenOnPostAsync_ThenRedirectionIsValid()
        {
            // Arrange
            _testSubject.ReturnUrl = ReturnUrl;

            // Act
            var result = await _testSubject.OnPostLoginAsync();

            // Assert
            Assert.True(result is RedirectResult);
            Assert.Equal(ReturnUrl, ((RedirectResult)result).Url);
        }

        [Fact]
        public async Task WhenOnPostCancelAsync_ThenRedirectionIsValid()
        {
            // Arrange
            const string expectedHomePage = "expectedHomePage";
            var authorizationRequest = new AuthorizationRequest
            {
                Client = new Client
                {
                    Properties = new Dictionary<string, string>
                    {
                        {"HomePage", expectedHomePage }
                    }
                }
            };

            _identityServerInteractionService
                .Setup(x => x.GetAuthorizationContextAsync(ReturnUrl))
                .ReturnsAsync(authorizationRequest);

            _testSubject.ReturnUrl = ReturnUrl;

            // Act
            var result = await _testSubject.OnPostCancelAsync();

            // Assert
            _identityServerInteractionService
                .Verify(
                    x => 
                        x.DenyAuthorizationAsync(authorizationRequest, AuthorizationError.AccessDenied, null),
                    Times.Once);

            Assert.True(result is RedirectResult);
            Assert.Equal(expectedHomePage, ((RedirectResult)result).Url);
        }
    }
}
