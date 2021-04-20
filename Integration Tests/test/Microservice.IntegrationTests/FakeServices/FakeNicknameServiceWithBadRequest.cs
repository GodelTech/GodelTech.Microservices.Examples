using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microservice.Application.Services;

namespace Microservice.IntegrationTests.FakeServices
{
    public class FakeNicknameServiceWithBadRequest : INicknameService
    {
        public Task<HttpResponseMessage> ExecuteAsync(IEnumerable<string> nicknames)
        {
            var httpResponseMessage =  new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = "Bad request",
                Content = new StringContent("Bad request")
            };

            return Task.FromResult(httpResponseMessage);
        }
    }
}
