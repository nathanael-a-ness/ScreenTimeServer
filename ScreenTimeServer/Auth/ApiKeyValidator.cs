using Microsoft.Extensions.Options;

namespace ScreenTimeServer.Auth
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        private readonly string _apiKey;
        public ApiKeyValidator(IOptions<ApiKeyOptions> options) 
        {
            _apiKey = options.Value.ApiKey;
        }

        public bool IsValid(string apiKey)
        {
            return apiKey == _apiKey;
        }
    }

    public class ApiKeyOptions
    {
        public string ApiKey { get; set; } = string.Empty;
    }
}
