using System;
using System.Threading.Tasks;
using GodelTech.Microservices.EntityFrameworkCore.Repositories;
using Microservice.Crm.v1.Contracts.Documents;
using Microservice.Crm.v1.Contracts.Requests;

namespace Microservice.Crm.v1.Resources.Clients
{
    public class CreateCommand : ICreateCommand
    {
        private readonly IRepository<DataLayer.Entities.Client> _clientRepository;

        public CreateCommand(IRepository<DataLayer.Entities.Client> clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public async Task<ClientDocument> ExecuteAsync(CreateClientRequest request)
        {
            var entity = new DataLayer.Entities.Client
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            await _clientRepository.AddAsync(entity);

            return new ClientDocument
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }
    }
}
