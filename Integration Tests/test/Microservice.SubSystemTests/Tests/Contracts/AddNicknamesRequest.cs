using System.Collections.Generic;
using System.Linq;

namespace Microservice.SubSystemTests.Tests.Contracts
{
    public class AddNicknamesRequest
    {
        public AddNicknamesRequest(
            string userName,
            IEnumerable<string> nicknames)
        {
            UserName = userName;
            Nicknames = nicknames ?? Enumerable.Empty<string>();
        }

        public string UserName { get; }

        public IEnumerable<string> Nicknames { get; }
    }
}
