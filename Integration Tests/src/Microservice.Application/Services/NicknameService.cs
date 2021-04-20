using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservice.Application.Services
{
    public class NicknameService : INicknameService
    {
        public const string HttpClientName = "Agify";

        private readonly IHttpClientFactory _httpClientFactory;

        public NicknameService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(IEnumerable<string> nicknames)
        {
            if (nicknames == null)
            {
                throw new ArgumentNullException(nameof(nicknames));
            }

            return await ExecuteInternalAsync(nicknames);
        }

        private async Task<HttpResponseMessage> ExecuteInternalAsync(IEnumerable<string> nicknames)
        {
            var client = _httpClientFactory.CreateClient(HttpClientName);

            return await client.GetAsync(BuildEventQuery(nicknames));
        }

        private string BuildEventQuery(IEnumerable<string> nicknames)
        {
            return $"?name[]={string.Join("&name[]=", nicknames)}";
        }
    }
}
