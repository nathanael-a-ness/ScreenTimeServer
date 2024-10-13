using System.Text.Json.Serialization;

namespace ScreenTimeServer.Data;

public class TicketEntity
{
    public string Id { get; set; } = "";
    [JsonPropertyName("exclamationResourceId")]
    public int ExclamationId { get; set; } = 2131361815;
    public string Note { get;set; } = "";
    public string earnedDate { get; set; } = "";
    public string Type { get; set; } = "";
    public string UsedDate { get; set; } = "";
    public bool Used { get; set; }
    public string Redemption { get; set; } = "";
    public int Time { get; set; } = 20;
}
