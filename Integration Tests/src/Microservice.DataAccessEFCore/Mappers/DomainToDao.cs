using Microservice.DataAccessEFCore.Models;
using DomainNickname = Microservice.Domain.Nickname;

namespace Microservice.DataAccessEFCore.Mappers
{
    public static class DomainToDao
    {
        public static Nickname ToDao(this DomainNickname nickname, int userId)
        {
            if (nickname == null)
            {
                return null;
            }

            return new Nickname
            {
                UserId = userId,
                Name = nickname.Name,
                Age = nickname.Age ?? default
            };
        }
    }
}
