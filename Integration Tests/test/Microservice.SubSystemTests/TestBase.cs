using System;
using Xunit;

namespace Microservice.SubSystemTests
{
    [Collection(nameof(StartUpFixture))]
    public class TestBase : IDisposable
    {
        protected TestBase(StartUpFixture fixture)
        {
            GodelTech.StoryLine.Wiremock.Config.ResetAll();

            fixture.ClearNicknamesInDatabase();
        }

        public void Dispose()
        {
            GodelTech.StoryLine.Wiremock.Config.ResetAll();
        }
    }
}