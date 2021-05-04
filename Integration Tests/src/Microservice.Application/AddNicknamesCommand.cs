using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microservice.Application.Services;
using Microservice.DataAccess.Interfaces;
using Microservice.Domain;

namespace Microservice.Application
{
    public class AddNicknamesCommand : IAddNicknamesCommand
    {
        private readonly INicknameService _nicknameService;
        private readonly IUserExists _userExists;
        private readonly IAddNicknames _addNicknames;

        public AddNicknamesCommand(
            INicknameService nicknameService,
            IUserExists userExists,
            IAddNicknames addNicknames)
        {
            _nicknameService = nicknameService;
            _userExists = userExists;
            _addNicknames = addNicknames;
        }

        public async Task<IEnumerable<string>> ExecuteAsync(
            string userName,
            IEnumerable<string> userNicknames)
        {
            Validate(userName, userNicknames);

            return await ExecuteInternalAsync(userName, userNicknames);
        }

        private async Task<IEnumerable<string>> ExecuteInternalAsync(
            string userName,
            IEnumerable<string> nicknames)
        {
            if (!await _userExists.ExecuteAsync(userName))
            {
                throw new ValidationException($"User {userName} is not found");
            }

            var agyfiNicknames = await GetAgyfyNicknames(nicknames);

            var validNickNames = GetValidNicknames(agyfiNicknames);

            await _addNicknames.ExecuteAsync(userName, validNickNames);

            return validNickNames.Select(x => x.Name);
        }

        private IEnumerable<Nickname> GetValidNicknames(IEnumerable<Nickname> agyfiNicknames)
        {
            if (agyfiNicknames == null)
            {
                return Enumerable.Empty<Nickname>();
            }

            return agyfiNicknames.Where(x => x.Age != null);
        }

        private async Task<IEnumerable<Nickname>> GetAgyfyNicknames(IEnumerable<string> nicknames)
        {
            try
            {
                var response = await _nicknameService.ExecuteAsync(nicknames);

                Validate(response, nicknames);

                var res = await response.Content.ReadAsStringAsync();
                return await JsonSerializer.DeserializeAsync<IEnumerable<Nickname>>(
                    await response.Content.ReadAsStreamAsync());
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ValidationException("External request to agyfy failed. Unexpected error");
            } 
        }

        private void Validate(HttpResponseMessage response, IEnumerable<string> nicknames)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ValidationException(
                    $"External request for nicknames {string.Join(", ", nicknames)} to agyfy failed." +
                    $" Status code {response.StatusCode}." +
                    $" Message {response.ReasonPhrase}");
            }
        }

        private void Validate(string userName, IEnumerable<string> userNicknames)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (userNicknames == null)
            {
                throw new ArgumentNullException(nameof(userNicknames));
            }
        }
    }
}
