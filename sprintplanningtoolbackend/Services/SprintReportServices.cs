namespace sprintplanningtoolbackend.Services
{
    using AutoMapper;
    using sprintplanningtoolbackend.Helpers;
    using sprintplanningtoolbackend.Models;

    public class SprintReportServices : ISprintReportServices
    {
        private DataContext _context;
        private readonly IMapper _mapper;


        public SprintReportServices(
                DataContext context,
                IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateReport(CreateReportRequest model)
        {
            // map model to a new sprint object
            var report = _mapper.Map<SprintReport>(model);
            report.CreatedDate = DateTime.Now.ToLocalTime();

            // save report
            _context?.SprintReports?.Add(report);
            _context?.SaveChanges();
        }

        public List<SprintReport> GetReportsByUsername(string username)
        {
            var reports = _context?.SprintReports?.Where(x => x.CreatedByUser == username)
                .ToList();

            // validation check
            if (reports == null) throw new AppException("Reports by " + username + "does not exist.");

            return reports;
        }

        public SprintReport GetReportById(int id)
        {
            var report = _context.SprintReports?.Find(id);
            if (report == null) throw new KeyNotFoundException("Sprint Report not found");
            return report;
        }        
        public SprintReport? GetRecentlyCreatedReport()
        {
            return _context.SprintReports.OrderByDescending(x => x.CreatedDate).ToList().First();
        }
        
        public IEnumerable<SprintReport>? GetAllReports()
        {
            return _context.SprintReports;
        }

        public void DeleteReport(int id)
        {
            var report = GetReportById(id);
            _context?.SprintReports?.Remove(report);
            _context?.SaveChanges();
        }

        public IEnumerable<TeamMember> GetTeamMembersFromReport(int id)
        {
            var teamMembers = _context?.TeamMembers;

            List<TeamMember> newMembers = new();

            foreach (var member in teamMembers)
            {
                if (id == member.SprintReportId)
                {
                    newMembers.Add(member);
                }
            }
            
            return newMembers;
        }        
        
        public SprintReport AddTeamMembersToReport(int id)
        {
            var team = GetTeamMembersFromReport(id);
            var report = GetReportById(id);

            foreach (var member in team)
            {
                report?.TeamMembers?.Add(member);
            }

            return report;
        }
    }
}
