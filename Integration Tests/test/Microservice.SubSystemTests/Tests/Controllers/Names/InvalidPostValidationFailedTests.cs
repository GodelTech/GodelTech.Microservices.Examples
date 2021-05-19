using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using GodelTech.StoryLine;
using GodelTech.StoryLine.Rest.Actions;
using GodelTech.StoryLine.Rest.Actions.Extensions;
using GodelTech.StoryLine.Rest.Expectations;
using GodelTech.StoryLine.Rest.Expectations.Extensions;
using Microservice.SubSystemTests.Tests.Contracts;

namespace Microservice.SubSystemTests.Tests.Controllers.Names
{
    public class InvalidPostValidationFailedTests : TestBase
    {
        public InvalidPostValidationFailedTests(StartUpFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void WhenPost_AndUserNicknamesIsNotProvided_ThenApiErrorIsReturned()
        {
            const string ErrorCode = "validation_error";
            const string Message = "Validation error for property Nicknames. Error message Nicknames must be provided in request";

            Scenario.New()
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("POST")
                    .Header("Content-Type", "application/json")
                    .Url("/names/add")
                    .JsonObjectBody(new AddNicknamesRequest(
                        "User",
                        null)))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.BadRequest)
                    .ReasonPhrase("Bad Request")
                    .JsonBody()
                    .MatchesObject(new ApiError(ErrorCode, Message), new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver()
                    }))
                .Run();
        }

        [Fact]
        public void WhenPost_AndNicknameAreEmpty_ThenApiErrorIsReturned()
        {
            const string ErrorCode = "validation_error";
            const string Message = "Validation error for property Nicknames. Error message Nicknames must be provided in request";

            Scenario.New()
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("POST")
                    .Header("Content-Type", "application/json")
                    .Url("/names/add")
                    .JsonObjectBody(new AddNicknamesRequest(
                        "User",
                        Enumerable.Empty<string>())))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.BadRequest)
                    .ReasonPhrase("Bad Request")
                    .JsonBody()
                    .MatchesObject(new ApiError(ErrorCode, Message), new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver() 
                    }))
                .Run();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public void WhenPost_AndUserNameIsNotProvided_ThenApiErrorIsReturned(string userName)
        {
            const string ErrorCode = "validation_error";
            const string Message = "Validation error for property User name. Error message User name must be provided in request";

            Scenario.New()
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("POST")
                    .Header("Content-Type", "application/json")
                    .Url("/names/add")
                    .JsonObjectBody(new AddNicknamesRequest(
                        userName,
                        new List<string>{ "some" })))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.BadRequest)
                    .ReasonPhrase("Bad Request")
                    .JsonBody()
                    .MatchesObject(new ApiError(ErrorCode, Message), new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver()
                    }))
                .Run();
        }
    }
}
