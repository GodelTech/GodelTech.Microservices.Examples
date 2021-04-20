using System.Threading.Tasks;

namespace Microservice.DataAccess.Interfaces
{
    public interface IGetTheYoungestNickname
    {
        Task<string> ExecuteAsync(string username);
    }
}
