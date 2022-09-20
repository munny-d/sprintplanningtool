namespace sprintplanningtoolbackend.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string? Username { get; set; }

        public int? SprintReportId { get; set; }
    }
}
