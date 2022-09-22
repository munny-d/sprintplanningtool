namespace sprintplanningtoolbackend.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReportRequest
    {
        [Required]
        public DateTime? SprintStartDate { get; set; }

        [Required]
        public DateTime? SprintEndDate { get; set; }

        public ICollection<TeamMember>? TeamMembers { get; set; }

        [Required]
        public int TeamSize { get; set; }

        public int AbsentDays { get; set; }

        public int WorkDays { get; set; }

        public int Capacity { get; set; }

        public int CarriedSP { get; set; }

        public int NewSP { get; set; }

        public int TotalSP { get; set; }

        [Required(ErrorMessage = "Please enter a sprint goal.")]
        public string? SprintGoal { get; set; }

        [Required(ErrorMessage = "A valid user must be logged in to create this report.")]
        public string? CreatedByUser { get; set; }
    }
}
