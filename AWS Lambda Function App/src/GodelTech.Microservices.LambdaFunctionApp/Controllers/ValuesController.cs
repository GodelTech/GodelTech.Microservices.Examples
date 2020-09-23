using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace GodelTech.Microservices.LambdaFunctionApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new []{ "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
