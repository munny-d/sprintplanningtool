using System.Text.Json.Serialization;

namespace sprintplanningtoolbackend.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string? Username { get; set; }

        [JsonIgnore]
        public int? SprintReportId { get; set; }
    }
}
