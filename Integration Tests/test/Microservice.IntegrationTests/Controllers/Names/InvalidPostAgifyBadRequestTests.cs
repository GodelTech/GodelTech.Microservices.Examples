using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microservice.Api.Models;
using Microservice.Application.Services;
using Microservice.DataAccessEFCore;
using Microservice.DataAccessEFCore.Models;
using Microservice.IntegrationTests.DefaultTestData;
using Microservice.IntegrationTests.Extensions;
using Microservice.IntegrationTests.FakeServices;

namespace Microservice.IntegrationTests.Controllers.Names
{
    public class InvalidPostAgifyBadRequestTests : IntegrationWebApplicationFactory
    {
        private readonly NicknamesContext _context;

        public InvalidPostAgifyBadRequestTests() : base(serviceConfiguration: services =>
        {
            services.ReplaceWithFake<INicknameService>(new FakeNicknameServiceWithBadRequest());
        })
        {
            _context = GetDbContext<NicknamesContext>();
            _context.AddDefaultUser();
        }

        [Fact]
        public async Task WhenPost_AndNicknameServiceReturnsError_ThenNicknamesAreNotAdded()
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

            var response = await client.PostAsync("names/add",
                new StringContent(request, Encoding.UTF8, "application/json"));
            var s = response.Content.ReadAsStream();
            var apiError = await JsonSerializer.DeserializeAsync<ApiError>(response.Content.ReadAsStream());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            AssertApiError(apiError, FirstNickname, SecondNickname, ThirdNickname);
            AssertDatabaseDoesNotContain(FirstNickname);
            AssertDatabaseDoesNotContain(SecondNickname);
            AssertDatabaseDoesNotContain(ThirdNickname);
        }

        private void AssertApiError(ApiError apiError, params string[] nicknames)
        {
            const string ErrorCode = "validation_error";
            var message = $"External request for nicknames {string.Join(", ", nicknames)} to agyfy failed." +
                     $" Status code {HttpStatusCode.BadRequest}." +
                     " Message Bad request";

            Assert.Equal(message, apiError.Message);
            Assert.Equal(ErrorCode, apiError.ErrorCode);
        }

        private void AssertDatabaseDoesNotContain(string name)
        {
            Assert.False(_context.Nicknames.Any(x =>
                x.Name.Equals(name)));
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
