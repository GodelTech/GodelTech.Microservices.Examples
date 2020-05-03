using System.Threading.Tasks;
using Microservice.Crm.v1.Contracts.Documents;

namespace Microservice.Crm.v1.Resources.Clients
{
    public interface IGetCommand
    {
        Task<ClientDocument> ExecuteAsync(int id);
    }
}