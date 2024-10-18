using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ScreenTimeServer.Auth
{
    public class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string ApiKeyHeaderName = "X-Api-Key";
        private readonly IApiKeyValidator _apiKeyValidator;

        public ApiKeyAuthorizationFilter(
            IApiKeyValidator apiKeyValidator
        )
        {
            _apiKeyValidator = apiKeyValidator;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var apiKey = context.HttpContext.Request.Headers[ApiKeyHeaderName];
#pragma warning disable CS8604 // Possible null reference argument.
            if (!_apiKeyValidator.IsValid(apiKey))
            {
                context.Result = new UnauthorizedResult();
            }
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
