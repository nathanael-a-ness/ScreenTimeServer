using Microsoft.AspNetCore.Mvc;

namespace ScreenTimeServer.Auth
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute() : base(typeof(ApiKeyAuthorizationFilter))
        {
        }
    }
}
