using System.Collections.Generic;
using System.Threading.Tasks;
using Microservice.Domain;

namespace Microservice.DataAccess.Interfaces
{
    public interface IAddNicknames
    {
        Task ExecuteAsync(string userName, IEnumerable<Nickname> userNicknames);
    }
}
