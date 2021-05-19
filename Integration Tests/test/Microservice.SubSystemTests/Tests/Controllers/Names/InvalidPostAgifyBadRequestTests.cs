using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;
using GodelTech.StoryLine;
using GodelTech.StoryLine.Rest.Actions;
using GodelTech.StoryLine.Rest.Actions.Extensions;
using GodelTech.StoryLine.Rest.Expectations;
using GodelTech.StoryLine.Rest.Expectations.Extensions;
using GodelTech.StoryLine.Wiremock.Actions;
using Microservice.SubSystemTests.Helpers;
using Microservice.SubSystemTests.Tests.Contracts;

namespace Microservice.SubSystemTests.Tests.Controllers.Names
{
    public class InvalidPostAgifyBadRequestTests : TestBase
    {
        private readonly AddNicknamesRequest _request;

        public InvalidPostAgifyBadRequestTests(StartUpFixture fixture) : base(fixture)
        {
            var nicknames = new List<string>
            {
                "Dark",
                "Knight",
                "Razor",
                "King"
            };

            _request = new AddNicknamesRequest("User", nicknames);
        }

        [Fact]
        public void WhenPost_AndNicknameServiceReturnsError_ThenNicknamesAreNotAdded()
        {
            const string ErrorCode = "validation_error";
            var message = $"External request for nicknames {string.Join(", ", _request.Nicknames)} to agyfy failed." +
                          $" Status code {HttpStatusCode.BadRequest}." +
                          " Message Bad Request";

            Scenario.New()
                .Given()
                .HasPerformed<MockHttpRequest>(x => x
                    .Request(req => req
                        .Method("GET")
                        .UrlPattern(TestHelpers.BuildUrlPattern(_request.Nicknames)))
                    .Response(res => res
                        .Status(HttpStatusCode.BadRequest)))
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("POST")
                    .Header("Content-Type", "application/json")
                    .Url("/names/add")
                    .JsonObjectBody(_request))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.BadRequest)
                    .ReasonPhrase("Bad Request")
                    .JsonBody()
                    .MatchesObject(new ApiError(ErrorCode, message), new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver()
                    }))
                .Run();
        }
    }
}
