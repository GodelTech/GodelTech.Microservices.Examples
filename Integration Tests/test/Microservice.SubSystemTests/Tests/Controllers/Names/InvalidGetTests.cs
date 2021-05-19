using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;
using GodelTech.StoryLine;
using GodelTech.StoryLine.Rest.Actions;
using GodelTech.StoryLine.Rest.Expectations;
using GodelTech.StoryLine.Rest.Expectations.Extensions;
using Microservice.SubSystemTests.Tests.Contracts;


namespace Microservice.SubSystemTests.Tests.Controllers.Names
{
    public class InvalidGetTests : TestBase
    {
        public InvalidGetTests(StartUpFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void WhenGet_AndNicknamesAreNotInDatabase_ThenApiErrorIsReturned()
        {
            const string ErrorCode = "validation_error";
            const string Message = "Nickname for user User is not found";

            Scenario.New()
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("GET")
                    .Header("Content-Type", "application/json")
                    .Url("/names/get/the_youngest/User"))
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
        public void WhenGet_AndUserIsNotInDatabase_ThenApiErrorIsReturned()
        {
            const string ErrorCode = "validation_error";
            const string Message = "User admin is not found";

            Scenario.New()
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("GET")
                    .Header("Content-Type", "application/json")
                    .Url("/names/get/the_youngest/admin"))
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
