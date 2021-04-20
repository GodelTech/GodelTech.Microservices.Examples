using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microservice.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microservice.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleException(context, exception);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            var (statusCode, apiError) = DetermineErrorResponseFromException(exception);

            var result = JsonSerializer.Serialize(apiError);

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }

        private static (HttpStatusCode statusCode, ApiError apiError) DetermineErrorResponseFromException(Exception exception) => exception switch
        {
            ValidationException validationException => (HttpStatusCode.BadRequest, BuildApiErrorFromValidationException(validationException)),
            _ => (HttpStatusCode.InternalServerError, BuildDefaultApiError())
        };

        private static ApiError BuildDefaultApiError()
        {
            return new ApiError
            {
                ErrorCode = "internal_server_error",
                Message = "Unexpected error is thrown"
            };
        }

        private static ApiError BuildApiErrorFromValidationException(ValidationException validationException)
        {
            const string ErrorCode = "validation_error";

            var message = CreateErrorMessage(validationException);

            return new ApiError()
            {
                ErrorCode = ErrorCode,
                Message = message
            };
        }

        private static string CreateErrorMessage(ValidationException validationException)
        {
            if (validationException.Errors == null || !validationException.Errors.Any())
            {
                return validationException.Message;
            }

            return string.Join("; ", validationException.Errors.Select(BuildErrorMessage));
        }

        private static string BuildErrorMessage(ValidationFailure validationFailure)
        {
            return $"Validation error for property {validationFailure.PropertyName}. Error message {validationFailure.ErrorMessage}";
        }
    }
}