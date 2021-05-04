using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservice.Application.Services
{
    public interface INicknameService
    {
        Task<HttpResponseMessage> ExecuteAsync(IEnumerable<string> nicknames);
    }
}
