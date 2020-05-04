using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodelTech.Microservices.Swagger.Swagger;
using Microservice.Crm.v1.Contracts.Documents;
using Microservice.Crm.v1.Contracts.Requests;
using Microservice.Crm.v1.Resources.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Crm.v1.Controllers
{
    [Route("v1/clients")]
    [Produces("application/json")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly Lazy<IGetCommand> _getCommand;
        private readonly Lazy<ICreateCommand> _createCommand;

        public ClientController(Lazy<IGetCommand> getCommand, Lazy<ICreateCommand> createCommand)
        {
            _getCommand = getCommand ?? throw new ArgumentNullException(nameof(getCommand));
            _createCommand = createCommand ?? throw new ArgumentNullException(nameof(createCommand));
        }

        [SwaggerRequiredScopes("Scope1")]
        [HttpPost]
        [ProducesResponseType(typeof(ClientDocument), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateClientRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var document = await _createCommand.Value.ExecuteAsync(request);

            return Created(
                Url.Action(nameof(GetAsync), new { id = document.Id }),
                document);
        }

        [SwaggerRequiredScopes("Scope1")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientDocument), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<ClientDocument> GetAsync(int id)
        {
            return await _getCommand.Value.ExecuteAsync(id);
        }
    }
}
