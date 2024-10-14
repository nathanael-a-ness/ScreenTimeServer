using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScreenTimeServer.Data
{
    public class StarGroupEntity
    {
        [Key]
        public string Id { get; set; } = "";
        public string Date { get; set; } = "";
        public string Note { get; set; } = "";
        public bool Used { get; set; }
        public int Earned { get; set; } = 0;
        [JsonIgnore]
        public ICollection<StarEntity>? Stars { get; set; }
    }
}