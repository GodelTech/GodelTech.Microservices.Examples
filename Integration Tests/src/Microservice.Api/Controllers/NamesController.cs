using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microservice.Api.Models;
using Microservice.Application;

namespace Microservice.Api.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private readonly IAddNicknamesCommand _addNicknamesCommand;
        private readonly IGetTheYoungestNicknameCommand _getTheYoungestNickname;
        private readonly IValidator<UserNicknames> _userNickNamesValidator;

        public NamesController(
            IGetTheYoungestNicknameCommand getTheYoungestNickname,
            IAddNicknamesCommand addNicknamesCommand,
            IValidator<UserNicknames> userNickNamesValidator)
        {
            _addNicknamesCommand = addNicknamesCommand;
            _getTheYoungestNickname = getTheYoungestNickname;
            _userNickNamesValidator = userNickNamesValidator;
        }

        [ProducesResponseType(typeof(IEnumerable<string>), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("add")]
        public async Task<IActionResult> AddNicknames([FromBody] UserNicknames userNicknames)
        {
            await _userNickNamesValidator.ValidateAndThrowAsync(userNicknames);

            var response = await _addNicknamesCommand.ExecuteAsync(
                userNicknames.UserName,
                userNicknames.Nicknames.Distinct());

            return Created(string.Empty, response);
        }

        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpGet("get/the_youngest/{userName}")]
        public async Task<IActionResult> GetTheYoungest(string userName)
        {
            Validate(userName);

            var response = await _getTheYoungestNickname.ExecuteAsync(userName);

            return Ok(response);
        }

        private void Validate(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ValidationException("User name must be provided");
            }
        }
    }
}
