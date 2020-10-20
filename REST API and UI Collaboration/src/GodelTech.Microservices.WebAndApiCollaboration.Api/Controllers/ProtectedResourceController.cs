using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodelTech.Microservices.WebAndApiCollaboration.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Weather API Policy")]
    public class ProtectedResourceController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Protected resource";
        }
    }
}
