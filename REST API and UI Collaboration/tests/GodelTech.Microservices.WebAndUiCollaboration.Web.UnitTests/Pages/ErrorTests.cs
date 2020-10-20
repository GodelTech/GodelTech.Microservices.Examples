using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;
using GodelTech.Microservices.Http.Exceptions;
using GodelTech.Microservices.WebAndApiCollaboration.Web.Pages;

namespace GodelTech.Microservices.WebAndApiCollaboration.Web.UnitTests.Pages
{
    public class ErrorTests
    {
        private readonly HttpContext _httpContext;
        private readonly ErrorModel _testSubject;

        public ErrorTests()
        {
            _httpContext = new DefaultHttpContext();

            _testSubject = new ErrorModel
            {
                PageContext = new PageContext
                {
                    HttpContext = _httpContext
                }
            };
        }

        [Fact]
        public void WhenOnGet_AndUnauthorizedExceptionIsThrown_ThenUnauthorizedPageIsShown()
        {
            // Arrange
            var collaborationException = new CollaborationException
            {
                StatusCode = 401
            };

            var exceptionHandlerPathFeature = new Mock<IExceptionHandlerPathFeature>();
            exceptionHandlerPathFeature.Setup(x => x.Error)
                .Returns(collaborationException);
            _httpContext.Features.Set(exceptionHandlerPathFeature.Object);

            // Act
            var result = _testSubject.OnGet();

            // Assert
            Assert.True(result is RedirectToPageResult);
            Assert.Equal("./Unauthorized", ((RedirectToPageResult) result).PageName);
        }

        [Fact]
        public void WhenOnGet_AndExceptionIsNotUnauthorized_ThenErrorPageIsShown()
        {
            // Arrange
            var collaborationException = new CollaborationException();

            var exceptionHandlerPathFeature = new Mock<IExceptionHandlerPathFeature>();
            exceptionHandlerPathFeature.Setup(x => x.Error)
                .Returns(collaborationException);
            _httpContext.Features.Set(exceptionHandlerPathFeature.Object);

            // Act
            var result = _testSubject.OnGet();

            // Assert
            Assert.True(result is PageResult);
        }

        [Fact]
        public void WhenOnGet_AndExceptionIsThrown_ThenValidRequestIdIsProvided()
        {
            // Arrange
            const string RequestId = "RequestId";
            _httpContext.TraceIdentifier = RequestId;

            // Act
            _testSubject.OnGet();

            // Assert
            Assert.True(_testSubject.ShowRequestId);
            Assert.Equal(RequestId, _testSubject.RequestId);
        }

        [Fact]
        public void WhenOnGet_AndExceptionIsThrown_AndRequestIdIsEmpty_ThenValidRequestIdIsNotShown()
        {
            // Arrange
            _httpContext.TraceIdentifier = string.Empty;

            // Act
            _testSubject.OnGet();

            // Assert
            Assert.False(_testSubject.ShowRequestId);
        }
    }
}
