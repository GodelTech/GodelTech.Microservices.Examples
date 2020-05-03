using System;
using System.Threading.Tasks;
using GodelTech.Microservices.Core.Exceptions;
using GodelTech.Microservices.EntityFrameworkCore.Repositories;
using Microservice.Crm.v1.Contracts.Documents;

namespace Microservice.Crm.v1.Resources.Clients
{
    public class GetCommand : IGetCommand
    {
        private readonly IRepository<DataLayer.Entities.Client> _clientRepository;

        public GetCommand(IRepository<DataLayer.Entities.Client> clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public async Task<ClientDocument> ExecuteAsync(int id)
        {
            var entity = await _clientRepository.GetByIdAsync(id);

            if (entity == null)
                throw new ResourceNotFoundException("Requested client was not found");

            return new ClientDocument
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }
    }
}
