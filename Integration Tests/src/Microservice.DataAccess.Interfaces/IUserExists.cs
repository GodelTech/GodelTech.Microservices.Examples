using System.Threading.Tasks;

namespace Microservice.DataAccess.Interfaces
{
    public interface IUserExists
    {
        Task<bool> ExecuteAsync(string userName);
    }
}
