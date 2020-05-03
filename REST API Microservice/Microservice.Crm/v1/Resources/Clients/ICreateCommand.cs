using System.Threading.Tasks;
using Microservice.Crm.v1.Contracts.Documents;
using Microservice.Crm.v1.Contracts.Requests;

namespace Microservice.Crm.v1.Resources.Clients
{
    public interface ICreateCommand
    {
        Task<ClientDocument> ExecuteAsync(CreateClientRequest request);
    }
}