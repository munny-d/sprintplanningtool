namespace sprintplanningtoolbackend.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using sprintplanningtoolbackend.Services;
    using sprintplanningtoolbackend.Models;
    using Microsoft.Extensions.Options;
    using sprintplanningtoolbackend.Helpers;

    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private ISprintReportService _reportService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ReportsController(
            ISprintReportService reportService,
            IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _reportService = reportService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Create a new sprint report
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/<ReportsController>/create
        [HttpPost("create")]
        public IActionResult CreateReport(CreateReportRequest model)
        {
            var user = User?.Identity?.Name;
            _reportService.CreateReport(model, user);
            return Ok(new { message = "Report is successfully created" });
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // GET: api/<ReportsController>/
        [HttpGet()]
        public IActionResult GetAllReports()
        {
            var reports = _reportService.GetAllReports();
            return Ok(reports);
        }

        /// <summary>
        /// Get report by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<ReportsController>/5
        [HttpGet("{id}")]
        public IActionResult GetReportById(int id)
        {
            var report = _reportService.GetReportById(id);
            return Ok(report);
        }        
        
        /// <summary>
        /// Get recently created report
        /// </summary>
        /// <returns></returns>
        // GET api/<ReportsController>/latest
        [HttpGet("latest")]
        public IActionResult GetLatest()
        {
            var report = _reportService.GetRecentlyCreatedReport();
            return Ok(report);
        }

        /// <summary>
        /// Delete report by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<ReportsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _reportService.DeleteReport(id);
            return Ok(new { message = "Report deleted successfully" });
        }
    }
}
