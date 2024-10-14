using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ScreenTimeServer.Data
{
    public class StarEntity
    {
        [Key]
        public string Id { get; set; } = "";
        [ForeignKey("Group")]
        [Column("Group")]
        public string GroupId { get; set; } = "";
        [JsonIgnore]
        public StarGroupEntity? Group { get; set; }
        public string Date { get; set; } = "";
        public string Note { get; set; } = "";
        
    }
}