using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodelTech.GraphQL.Web.GraphQL;
using GraphQL;
using GraphQL.FluentValidation;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GodelTech.GraphQL.Web.Controllers
{
    [Route("GraphQL")]
    [ApiController]
    public class GraphQLController : Controller
    {
        private readonly ValidatorTypeCache _cache;
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ILogger<GraphQLController> _logger;

        public GraphQLController(
            ValidatorTypeCache cache,
            ISchema schema,
            IDocumentExecuter documentExecuter,
            ILogger<GraphQLController> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _schema = schema ?? throw new ArgumentNullException(nameof(schema));
            _documentExecuter = documentExecuter ?? throw new ArgumentNullException(nameof(documentExecuter));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();

            var executionResult = await _documentExecuter.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
                _.UseFluentValidation(_cache);
            });

            if (executionResult.Errors?.Count > 0)
            {
                var errors = new List<object>();

                foreach (var error in executionResult.Errors)
                {
                    _logger.LogError(error.InnerException, error.Message);
                    errors.Add(new { error.Code, error.Message });
                }

                return BadRequest(new { Errors = errors });
            }

            return Ok(new { executionResult.Data });
        }
    }
}