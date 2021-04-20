using System;
using System.Threading.Tasks;
using FluentValidation;
using Microservice.DataAccess.Interfaces;

namespace Microservice.Application
{
    public class GetTheYoungestNicknameCommand : IGetTheYoungestNicknameCommand
    {
        private readonly IUserExists _userExists;
        private readonly IGetTheYoungestNickname _getTheYoungestNickname;

        public GetTheYoungestNicknameCommand(IUserExists userExists, IGetTheYoungestNickname getTheYoungestNickname)
        {
            _userExists = userExists;
            _getTheYoungestNickname = getTheYoungestNickname;
        }

        public async Task<string> ExecuteAsync(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (!await _userExists.ExecuteAsync(userName))
            {
                throw new ValidationException($"User {userName} is not found");
            }

            var nickName = await _getTheYoungestNickname.ExecuteAsync(userName);

            ValidateNickname(nickName, userName);

            return nickName;
        }

        private void ValidateNickname(string nickName, string userName)
        {
            if (string.IsNullOrEmpty(nickName))
            {
                throw new ValidationException($"Nickname for user {userName} is not found");
            }
        }
    }
}
