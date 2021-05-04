using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microservice.Application.Services;
using Microservice.Domain;

namespace Microservice.IntegrationTests.FakeServices
{
    public class FakeNicknameServiceWithValidRequest : INicknameService
    {
        public Task<HttpResponseMessage> ExecuteAsync(IEnumerable<string> nicknames)
        {
            var fakeNicknames = nicknames.Select((x, i) => new Nickname(x, i + 1));

            var httpResponseMessage =  new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(fakeNicknames))
            };

            return Task.FromResult(httpResponseMessage);
        }
    }
}
