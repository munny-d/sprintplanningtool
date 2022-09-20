namespace sprintplanningtoolbackend.Services
{
    using sprintplanningtoolbackend.Models;

    public interface ISprintReportServices
    {
        void CreateReport(CreateReportRequest sprintReport);
        List<SprintReport> GetReportsByUsername (string username);
        SprintReport GetReportById (int id);
        SprintReport GetRecentlyCreatedReport ();
        IEnumerable<SprintReport> GetAllReports ();
        void DeleteReport(int id);
        IEnumerable<TeamMember> GetTeamMembersFromReport(int id);
        SprintReport AddTeamMembersToReport(int id);

    }
}
