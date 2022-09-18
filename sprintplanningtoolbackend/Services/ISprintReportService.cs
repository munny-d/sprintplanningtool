namespace sprintplanningtoolbackend.Services
{
    using sprintplanningtoolbackend.Models;

    public interface ISprintReportService
    {
        void CreateReport(CreateReportRequest sprintReport, string user);
        List<SprintReport> GetReportsByUsername (string username);
        SprintReport GetReportById (int id);
        SprintReport GetRecentlyCreatedReport ();
        IEnumerable<SprintReport> GetAllReports ();
        void DeleteReport(int id);
    }
}
