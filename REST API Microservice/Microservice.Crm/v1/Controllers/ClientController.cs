using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodelTech.Microservices.Core.Mvc.Swagger;
using Microservice.Crm.v1.Contracts.Documents;
using Microservice.Crm.v1.Contracts.Requests;
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
        [Authorize(Startup.Policies.JobsManage)]
        [SwaggerRequiredScopes(Startup.Scopes.JobsManage)]
        [HttpPost]
        [ProducesResponseType(typeof(ClientDocument), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Guid jobId, [FromBody] CreateClientRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // TODO: Put your logic here

            return Created(
                "http://url.com",
                new ClientDocument());
        }

        [Authorize(Startup.Policies.JobsRead)]
        [SwaggerRequiredScopes(Startup.Scopes.JobsRead)]
        [HttpGet("{activityId}")]
        [ProducesResponseType(typeof(ClientDocument), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<ClientDocument> GetAsync(Guid jobId, Guid activityId)
        {
            // TODO: Put your logic here

            return new ClientDocument();
        }
    }
}
