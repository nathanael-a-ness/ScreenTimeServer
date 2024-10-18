namespace ScreenTimeServer.Auth
{
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}