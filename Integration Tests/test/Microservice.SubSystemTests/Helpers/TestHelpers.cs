using System.Collections.Generic;
using System.Linq;
using Microservice.SubSystemTests.Tests.Contracts;

namespace Microservice.SubSystemTests.Helpers
{
    public static class TestHelpers
    {
        public static string BuildUrlPattern(IEnumerable<string> nicknames)
        {
            return @$".*\?name\[\]={string.Join(@"&name\[\]=", nicknames)}";
        }

        public static IEnumerable<Nickname> BuildResponse(IEnumerable<string> nicknames, int offset = 0)
        {
            return nicknames.Select((x, i) => new Nickname(x, i + offset));
        }
    }
}
