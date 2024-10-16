namespace ScreenTimeServer.Data
{
    static public class Utility
    {
        public static DateTime GetToday()
        {
            return DateTime.Now.Date.ToUniversalTime();
        }
    }
}
