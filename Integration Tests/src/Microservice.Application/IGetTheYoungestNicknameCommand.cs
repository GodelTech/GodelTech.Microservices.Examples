using System.Threading.Tasks;

namespace Microservice.Application
{
    public interface IGetTheYoungestNicknameCommand
    {
        Task<string> ExecuteAsync(string userName);
    }
}
