using System.Collections.Generic;
using System.Net;
using Xunit;
using GodelTech.StoryLine;
using GodelTech.StoryLine.Rest.Actions;
using GodelTech.StoryLine.Rest.Actions.Extensions;
using GodelTech.StoryLine.Rest.Expectations;
using GodelTech.StoryLine.Rest.Expectations.Extensions;
using GodelTech.StoryLine.Wiremock.Actions;
using GodelTech.StoryLine.Wiremock.Builders;
using Microservice.SubSystemTests.Helpers;
using Microservice.SubSystemTests.Tests.Contracts;

namespace Microservice.SubSystemTests.Tests.Controllers.Names
{
    public class ValidGetTests : TestBase
    {
        private readonly AddNicknamesRequest _request;

        public ValidGetTests(StartUpFixture fixture) : base(fixture)
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
        public void WhenGet_AndNicknamesExists_ThenTheYoungestNicknameIsReturned()
        {
            Scenario.New()
                    .Given()
                    .HasPerformed<MockHttpRequest>(x => x
                        .Request(req => req
                            .Method("GET")
                            .UrlPattern(TestHelpers.BuildUrlPattern(_request.Nicknames)))
                            .Response(res => res
                            .Status(HttpStatusCode.OK)
                            .JsonObjectBody(TestHelpers.BuildResponse(_request.Nicknames))))
                    .When()
                    .Performs<HttpRequest>(req => req
                        .Method("POST")
                        .Header("Content-Type", "application/json")
                        .Url("/names/add")
                        .JsonObjectBody(_request))
                    .Performs<HttpRequest>(req => req
                        .Method("GET")
                        .Header("Content-Type", "application/json")
                        .Url("/names/get/the_youngest/User"))
                    .Then()
                    .Expects<HttpResponse>(res => res
                        .Status(HttpStatusCode.OK)
                        .ReasonPhrase("OK")
                        .JsonBody()
                        .MatchesObject("Dark"))
                    .Run();
        }
    }
}
