namespace sprintplanningtoolbackend.Models
{
    public class SprintReport
    {
        public DateTime SprintStartDate { get; set; }
        public DateTime SprintEndDate { get; set; }
        public List<User>? TeamMembers { get; set; }
        public int? TeamSize { get; set; }
        public int? AbsentDays { get; set; }
        public int? WorkDays { get; set; }
        public int? Capacity { get; set; }
        public int? NewSP { get; set; }
        public int? CarriedSP { get; set; }
        public int? TotalSP { get; set; }
        public string? SprintGoal { get; set; }

        public string? CreatedByUser { get; set; }
    }
}
