using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microservice.Api.Models;
using Microservice.DataAccessEFCore;
using Microservice.IntegrationTests.DefaultTestData;
using Microservice.IntegrationTests.Extensions;

namespace Microservice.IntegrationTests.Controllers.Names
{
    public class InvalidGetTests : IntegrationWebApplicationFactory
    {
        private readonly NicknamesContext _context;

        public InvalidGetTests()
        {
            _context = GetDbContext<NicknamesContext>();
        }

        [Fact]
        public async Task WhenGet_AndNicknamesAreNotInDatabase_ThenApiErrorIsReturned()
        {
            _context.AddDefaultUser();
            const string ErrorCode = "validation_error";
            const string Message = "Nickname for user admin is not found";

            var user = DataAccessTestData.DefaultUser;

            var client = CreateClient();

            var response = await client.GetAsync($"names/get/the_youngest/{user.Name}");
            var apiError = await JsonSerializer.DeserializeAsync<ApiError>(response.Content.ReadAsStream());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ErrorCode, apiError.ErrorCode);
            Assert.Equal(Message, apiError.Message);
        }

        [Fact]
        public async Task WhenGet_AndUserIsNotInDatabase_ThenApiErrorIsReturned()
        {
            const string ErrorCode = "validation_error";
            const string Message = "User admin is not found";

            var user = DataAccessTestData.DefaultUser;

            var client = CreateClient();

            var response = await client.GetAsync($"names/get/the_youngest/{user.Name}");
            var apiError = await JsonSerializer.DeserializeAsync<ApiError>(response.Content.ReadAsStream());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ErrorCode, apiError.ErrorCode);
            Assert.Equal(Message, apiError.Message);
        }
    }
}
