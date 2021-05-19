using Xunit;

namespace Microservice.SubSystemTests
{
    [CollectionDefinition(nameof(StartUpFixture))]
    public sealed class StartUpFixtureCollection : ICollectionFixture<StartUpFixture>
    {
    }
}