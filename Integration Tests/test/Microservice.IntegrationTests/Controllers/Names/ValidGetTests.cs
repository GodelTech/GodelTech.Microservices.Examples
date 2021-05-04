using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Microservice.DataAccessEFCore;
using Microservice.DataAccessEFCore.Models;
using Microservice.IntegrationTests.DefaultTestData;
using Microservice.IntegrationTests.Extensions;

namespace Microservice.IntegrationTests.Controllers.Names
{
    public class ValidGetTests : IntegrationWebApplicationFactory
    {
        private readonly NicknamesContext _context;

        public ValidGetTests()
        {
            _context = GetDbContext<NicknamesContext>();
            _context.AddDefaultUser();
        }

        [Fact]
        public async Task WhenGet_AndNicknamesAreInDatabase_ThenTheYoungestNicknameIsReturned()
        {
            const string TheYoungestNickname = "TheYoungestNickName";
            var user = DataAccessTestData.DefaultUser;

            AddNicknamesIntoContext(user, TheYoungestNickname, "SecondNickName", "ThirdNickname");

            var client = CreateClient();

            var response = await client.GetAsync($"names/get/the_youngest/{user.Name}");
            var actualNickname = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal($"\"{TheYoungestNickname}\"", actualNickname);
        }

        private void AddNicknamesIntoContext(User user, params string[] nicknames)
        {
            var daoNicknames = nicknames.Select((x, i) => new Nickname
            {
                Age = i+100,
                Name = x,
                UserId = user.Id
            });

            _context.AddNicknames(daoNicknames);
        }
    }
}
