namespace Microservice.SubSystemTests.Tests.Contracts
{
    public class ApiError
    {
        public ApiError(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public string ErrorCode { get; }

        public string Message { get; }
    }
}
