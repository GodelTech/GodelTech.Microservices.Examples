using System.IO;
using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using Xunit;

namespace GodelTech.Microservices.LambdaFunctionApp.Integration.Tests
{
    public class ValuesControllerTests
    {
        private readonly LambdaEntryPoint _lambdaFunction;
        private readonly TestLambdaContext _testLambdaContext;

        public ValuesControllerTests()
        {
            _lambdaFunction = new LambdaEntryPoint();
            _testLambdaContext = new TestLambdaContext();
        }

        [Fact]
        public async Task TestGet()
        {
            // Arrange
            var request = GetRequest("./SampleRequests/ValuesController-Get.json");
            var response = await _lambdaFunction.FunctionHandlerAsync(request, _testLambdaContext);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("[\"value1\",\"value2\"]", response.Body);
            Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
            Assert.Equal("application/json; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
        }

        [Fact]
        public async Task TestGetWithParameter()
        {
            // Arrange
            var request = GetRequest("./SampleRequests/ValuesController-Get-With-Parameter.json");
            var response = await _lambdaFunction.FunctionHandlerAsync(request, _testLambdaContext);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal("value", response.Body);
            Assert.True(response.MultiValueHeaders.ContainsKey("Content-Type"));
            Assert.Equal("text/plain; charset=utf-8", response.MultiValueHeaders["Content-Type"][0]);
        }

        private APIGatewayProxyRequest GetRequest(string requestPath)
        {
            var request = File.ReadAllText(requestPath);
            return JsonConvert.DeserializeObject<APIGatewayProxyRequest>(request);
        }
    }
}
