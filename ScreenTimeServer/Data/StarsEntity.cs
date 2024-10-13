namespace ScreenTimeServer.Data
{
    public class StarsEntity
    {
        public Guid Id { get; set; }
        public string Date { get; set; } = "";
        public string Note { get; set; } = "";
        public bool Used { get; set; }
    }
}