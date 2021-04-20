using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microservice.IntegrationTests.DefaultTestData;
using Microservice.IntegrationTests.Extensions;
using Microservice.IntegrationTests.FakeServices;
using Microservice.Application.Services;
using Microservice.Application.Models;
using Microservice.DataAccessEFCore;
using Microservice.DataAccessEFCore.Models;

namespace Microservice.IntegrationTests.Controllers.Names
{
    public class ValidPostTests : IntegrationWebApplicationFactory
    {
        private readonly NicknamesContext _context;

        public ValidPostTests() : base(services =>
             {
                 services.ReplaceWithFake<INicknameService>(new FakeNicknameServiceWithValidRequest());
             })
        {
            _context = GetDbContext<NicknamesContext>();
            _context.AddDefaultUser();
        }

        [Fact]
        public async Task WhenPost_AndNicknameServiceIsCalledSuccessfully_ThenNicknamesAreAdded()
        {
            const string FirstNickname = "FirstNickname";
            const string ThirdNickname = "ThirdNickname";
            const string SecondNickname = "SecondNickName";

            var user = DataAccessTestData.DefaultUser;

            var client = CreateClient();

            var request = JsonSerializer.Serialize(
                BuildAddNicknamesRequest(
                    user,
                    FirstNickname,
                    SecondNickname,
                    ThirdNickname));

            var response = await client.PostAsync("names/add", new StringContent(request, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            AssertDatabaseContains(FirstNickname, 1, user.Id);
            AssertDatabaseContains(SecondNickname, 2, user.Id);
            AssertDatabaseContains(ThirdNickname, 3, user.Id);
        }

        [Fact]
        public async Task WhenPost_AndNicknamesIsInDatabase_ThenAgeIsUpdated()
        {
            var user = DataAccessTestData.DefaultUser;
            var existedNickname = DataAccessTestData.ExistedNickname;

            _context.AddNickname(existedNickname);

            var client = CreateClient();

            var request = JsonSerializer.Serialize(
                BuildAddNicknamesRequest(user, existedNickname.Name));

            var response = await client.PostAsync("names/add", new StringContent(request, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            AssertDatabaseContains(existedNickname.Name, 1, user.Id);
        }

        private void AssertDatabaseContains(string name, int age, int userId)
        {
            var nickNames = _context.Nicknames.ToList();
            Assert.True(_context.Nicknames.Any(x =>
                x.Name.Equals(name)
                && x.Age.Equals(age)
                && x.UserId.Equals(userId)));
        }

        private UserNicknames BuildAddNicknamesRequest(User user, params string[] nicknames)
        {
            return new UserNicknames
            {
                UserName = user.Name,
                Nicknames = nicknames
            };
        }
    }
}
