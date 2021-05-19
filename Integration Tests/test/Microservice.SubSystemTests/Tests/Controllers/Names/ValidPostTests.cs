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
    public sealed class ValidPostTests : TestBase
    { 
        private readonly AddNicknamesRequest _request;
        private readonly AddNicknamesRequest _secondRequest;

        public ValidPostTests(StartUpFixture fixture) : base(fixture)
        {
            var nicknamesForFirstRequest = new List<string>
            {
                "Dark",
                "Knight",
                "Razor",
                "King"
            };

            var nicknamesForSecondRequest = new List<string>
            {
                "Knight",
                "New"

            };

            _request = new AddNicknamesRequest("User", nicknamesForFirstRequest);
            _secondRequest = new AddNicknamesRequest("User", nicknamesForSecondRequest);
        }

        [Fact]
        public void WhenAddNewNicknames_ThenNicknamesShouldBeAddedSuccessfully()
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
                    .Then()
                    .Expects<HttpResponse>(res => res
                        .Status(HttpStatusCode.Created)
                        .ReasonPhrase("Created")
                        .JsonBody()
                        .MatchesObject(_request.Nicknames))
                    .Run();
        }

        [Fact]
        public void WhenAddExistingNicknames_ThenNicknamesShouldBeAddedSuccessfully()
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
                .HasPerformed<MockHttpRequest>(x => x
                    .Request(req => req
                        .Method("GET")
                        .UrlPattern(TestHelpers.BuildUrlPattern(_secondRequest.Nicknames)))
                    .Response(res => res
                        .Status(HttpStatusCode.OK)
                        .JsonObjectBody(TestHelpers.BuildResponse(_secondRequest.Nicknames, 10))))
                .When()
                .Performs<HttpRequest>(req => req
                    .Method("POST")
                    .Header("Content-Type", "application/json")
                    .Url("/names/add")
                    .JsonObjectBody(_request))
                .Performs<HttpRequest>(req => req
                    .Method("POST")
                    .Header("Content-Type", "application/json")
                    .Url("/names/add")
                    .JsonObjectBody(_secondRequest))
                .Then()
                .Expects<HttpResponse>(res => res
                    .Status(HttpStatusCode.Created)
                    .ReasonPhrase("Created")
                    .JsonBody()
                    .MatchesObject(_secondRequest.Nicknames))
                .Run();
        }
    }
}