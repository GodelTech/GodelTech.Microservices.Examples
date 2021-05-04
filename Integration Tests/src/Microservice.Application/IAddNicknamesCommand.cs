using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservice.Application
{
    public interface IAddNicknamesCommand
    {
        Task<IEnumerable<string>> ExecuteAsync(string userName, IEnumerable<string> userNicknames);
    }
}
