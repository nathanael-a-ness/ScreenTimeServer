using System.Text.Json.Serialization;

namespace ScreenTimeServer.Data;

public class TicketEntity
{
    public string Id { get; set; } = "";
    public int ExclamationId { get; set; } = 0;
    public string Note { get;set; } = "";
    public DateTime earnedDate { get; set; } = DateTime.MinValue;
    public string Type { get; set; } = "";
    public DateTime UsedDate { get; set; } = DateTime.MinValue;
    public bool Used { get; set; }
    public string Redemption { get; set; } = "";
    public int Time { get; set; } = 20;
}
