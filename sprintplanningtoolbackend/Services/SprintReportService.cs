namespace sprintplanningtoolbackend.Services
{
    using AutoMapper;
    using sprintplanningtoolbackend.Helpers;
    using sprintplanningtoolbackend.Models;

    public class SprintReportService : ISprintReportService
    {
        private DBContext _context;
        private readonly IMapper _mapper;


        public SprintReportService(
                DBContext context,
                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateReport(SprintReport model)
        {
            // map model to new user object
            var report = _mapper.Map<SprintReport>(model);

            // save report
            _context.SprintReports.Add(report);
            _context.SaveChanges();
        }        
        
        public List<SprintReport> GetReportsByUsername(string username)
        {
            var reports = _context.SprintReports.Where(x => x.CreatedByUser == username)
                .ToList();

            // validation check
            if (reports == null) throw new AppException("Reports by " + username + "does not exist.");

            return reports;
        }

        public SprintReport GetReportById(int id)
        {
            var report = _context.SprintReports.Find(id);
            if (report == null) throw new KeyNotFoundException("Sprint Report not found");
            return report;
        }        
        public SprintReport GetRecentlyCreatedReport()
        {
            return _context.SprintReports.FirstOrDefault();
        }
        
        public IEnumerable<SprintReport> GetAllReports()
        {
            return _context.SprintReports;
        }

        public void DeleteReport(int id)
        {
            var report = GetReportById(id);
            _context.SprintReports.Remove(report);
            _context.SaveChanges();
        }
        
    }
}
