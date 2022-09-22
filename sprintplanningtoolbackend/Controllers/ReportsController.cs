using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using sprintplanningtoolbackend.Services;
using sprintplanningtoolbackend.Models;
using Microsoft.Extensions.Options;
using sprintplanningtoolbackend.Helpers;
using System.Security.Claims;

namespace sprintplanningtoolbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private ISprintReportServices _reportService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ReportsController(
            ISprintReportServices reportService,
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
            _reportService.CreateReport(model);
            return Ok(new { message = "Report is successfully created" });
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        // GET: api/<ReportsController>/
        [HttpGet()]
        public IActionResult GetAllReports()
        {
            var reports = _reportService.GetAllReports();

            foreach (var report in reports)
            {
                _reportService.AddTeamMembersToReport(report.Id);
            }
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
            _reportService.AddTeamMembersToReport(id);
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
            _reportService.AddTeamMembersToReport(report.Id);
            return Ok(report);
        }

        /// <summary>
        /// Get team members by sprint report id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<ReportsController>/team
        [HttpGet("team/{id}")]
        public IActionResult GetTeamById(int sprintId)
        {
            var team = _reportService.GetTeamMembersFromReport(sprintId);
            return Ok(team);
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
