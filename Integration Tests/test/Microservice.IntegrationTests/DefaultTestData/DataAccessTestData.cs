using Microservice.DataAccessEFCore.Models;

namespace Microservice.IntegrationTests.DefaultTestData
{
    public static class DataAccessTestData
    {
        public static User DefaultUser => new User
        {
            Id = 1,
            Name = "admin"
        };

        public static Nickname ExistedNickname => new Nickname
        {
            UserId = 1,
            Name = "ExistedNickname",
            Age = 99
        };
    }
}
