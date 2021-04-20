using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Microservice.Api.Models;

namespace Microservice.IntegrationTests.Controllers.Names
{
    public class InvalidPostValidationFailedTests : IntegrationWebApplicationFactory
    {
        [Fact]
        public async Task WhenPost_AndNicknameAreEmpty_ThenApiErrorIsReturned()
        {
            const string ErrorCode = "validation_error";
            const string Message = "Validation error for property Nicknames. Error message Nicknames must be provided in request";

            var client = CreateClient();

            var request = JsonSerializer.Serialize(
                BuildAddNicknamesRequest("UserName"));

            var response = await client.PostAsync("names/add",
                new StringContent(request, Encoding.UTF8, "application/json"));

            var apiError = await JsonSerializer.DeserializeAsync<ApiError>(response.Content.ReadAsStream());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(Message, apiError.Message);
            Assert.Equal(ErrorCode, apiError.ErrorCode);
        }

        [Fact]
        public async Task WhenPost_AndNicknameAreNull_ThenApiErrorIsReturned()
        {
            const string ErrorCode = "validation_error";
            const string Message = "Validation error for property Nicknames. Error message Nicknames must be provided in request";

            var client = CreateClient();

            var request = JsonSerializer.Serialize(
                BuildAddNicknamesRequest("UserName", null));

            var response = await client.PostAsync("names/add",
                new StringContent(request, Encoding.UTF8, "application/json"));

            var apiError = await JsonSerializer.DeserializeAsync<ApiError>(response.Content.ReadAsStream());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(Message, apiError.Message);
            Assert.Equal(ErrorCode, apiError.ErrorCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        public async Task WhenPost_AndUserNameIsNotProvided_ThenApiErrorIsReturned(string userName)
        {
            const string ErrorCode = "validation_error";
            const string Message = "Validation error for property User name. Error message User name must be provided in request";

            var client = CreateClient();

            var request = JsonSerializer.Serialize(
                BuildAddNicknamesRequest(userName, "nickname"));

            var response = await client.PostAsync("names/add",
                new StringContent(request, Encoding.UTF8, "application/json"));

            var apiError = await JsonSerializer.DeserializeAsync<ApiError>(response.Content.ReadAsStream());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(Message, apiError.Message);
            Assert.Equal(ErrorCode, apiError.ErrorCode);
        }

        [Fact]
        public async Task WhenPost_AndUserNicknamesIsNotProvided_ThenApiErrorIsReturned()
        {
            const string ErrorCode = "validation_error";

            var client = CreateClient();

            var response = await client.PostAsync("names/add",
                new StringContent("{}", Encoding.UTF8, "application/json"));

            var apiError = await JsonSerializer.DeserializeAsync<ApiError>(response.Content.ReadAsStream());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(ErrorCode, apiError.ErrorCode);
        }

        private UserNicknames BuildAddNicknamesRequest(string userName, params string[] nicknames)
        {
            return new UserNicknames
            {
                UserName = userName,
                Nicknames = nicknames
            };
        }
    }
}
