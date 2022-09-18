namespace sprintplanningtoolbackend.Services
{
    using sprintplanningtoolbackend.Models;

    public interface ISprintReportService
    {
        void CreateReport(SprintReport sprintReport);
        List<SprintReport> GetReportsByUsername (string username);
        SprintReport GetReportById (int id);
        SprintReport GetRecentlyCreatedReport ();
        IEnumerable<SprintReport> GetAllReports ();
        void DeleteReport(int id);
    }
}
