using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using sprintplanningtoolbackend.Helpers;
using sprintplanningtoolbackend.Models;
using sprintplanningtoolbackend.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace sprintplanningtooltests
{
    public class SprintReportServicesTests
    {

        private SprintReportServices _subject;
        private readonly IMapper _mapper;
        private readonly Mock<DbSet<SprintReport>> _mockReports;
        private readonly Mock<DataContext> _context;
        private int callCount = 0;
        private int save = 0;

        public SprintReportServicesTests()
        {
            _mockReports = new Mock<DbSet<SprintReport>>();
            _context = new Mock<DataContext>(It.IsAny<ConfigurationProvider>());

            // Set up mapper
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
            _mapper = mockMapper.CreateMapper();

            // Create UserService instance
            _subject = new SprintReportServices(_context.Object, _mapper);

        }

        // Helper method for setting up a mock DB set for Moq DB Context
        private Mock<DbSet<SprintReport>> SetUpDBReports(IQueryable<SprintReport> reports)
        {
            _mockReports.As<IQueryable<SprintReport>>().Setup(m => m.Provider).Returns(reports.Provider);
            _mockReports.As<IQueryable<SprintReport>>().Setup(m => m.Expression).Returns(reports.Expression);
            _mockReports.As<IQueryable<SprintReport>>().Setup(m => m.ElementType).Returns(reports.ElementType);
            _mockReports.As<IQueryable<SprintReport>>().Setup(m => m.GetEnumerator()).Returns(reports.GetEnumerator());
            
            return _mockReports;
        }

        #region GetAllReports

        [Fact]
        public void GetAllReports_WhenNoReportsExist_ReturnNothing()
        {
            // Arrange
            var reports = new List<SprintReport>().AsQueryable();
            SetUpDBReports(reports);
            _context.Setup(c => c.SprintReports).Returns(_mockReports.Object);

            // Act
            var result = _subject.GetAllReports().ToList();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllReports_WhenOneReportExists_ReturnsOneReport()
        {
            // Arrange
            var reports = new List<SprintReport>
            {
                new SprintReport 
                { 
                    CreatedByUser = "test", 
                    Id = 1 
                } 
            }.AsQueryable();

            SetUpDBReports(reports);

            _context.Setup(c => c.SprintReports).Returns(_mockReports.Object);

            // Act
            var result = _subject.GetAllReports().ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.True(result.Count == 1);
            Assert.Equal(reports, result);
        }

        [Fact]
        public void GetAllReports_WhenThreeReportsExists_ReturnsThreeReports()
        {
            // Arrange
            var reports = new List<SprintReport>
            {
                new SprintReport {
                    Id = 1,
                },
                new SprintReport {
                    Id = 2,
                },
                new SprintReport {
                    Id = 3,
                },
            }.AsQueryable();

            SetUpDBReports(reports);
            _context.Setup(c => c.SprintReports).Returns(_mockReports.Object);

            // Act
            var result = _subject.GetAllReports().ToList();

            // Assert
            Assert.NotEmpty(result);
            Assert.True(result.Count == 3);
            Assert.Equal(reports, result);
        }


        #endregion

        #region GetReportById

        [Fact]
        public void GetReportById_WhenIdIsFive_ThrowsKeyNotFoundEx()
        {
            // Arrange
            var reports = new List<SprintReport>
            {
                new SprintReport {
                    Id = 1,
                },
                new SprintReport {
                    Id = 2,
                },
            }.AsQueryable();

            SetUpDBReports(reports);
            _context.Setup(c => c.SprintReports).Returns(_mockReports.Object);

            var expected = "Sprint Report not found";

            // Act + Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => _subject.GetReportById(1));
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public void GetReportById_WhenIdIsOne_ReturnsReportCreatedByTestUser()
        {
            // Arrange
            var report = new SprintReport
            {
                CreatedByUser = "test",
                Id = 1,
            };

            var reports = new List<SprintReport> { report }.AsQueryable();

            SetUpDBReports(reports);
            _mockReports.Setup(m => m.Find(report.Id)).Returns(report);
            _context.Setup(c => c.SprintReports).Returns(_mockReports.Object);

            var expected = reports.Where(u => u.Id == 1).Single();

            // Act
            var result = _subject.GetReportById(1);

            // Assert
            Assert.True(expected == result);
        }


        #endregion

        #region CreateReport

        [Fact]
        public void CreateReport_WhenValidInput_AddAndSaveChangesAreInvoked()
        {
            // Arrange
            var report = new CreateReportRequest()
            {
                CreatedByUser = "test",
            };

            var reports = new List<SprintReport> { new SprintReport { Id = 1 } }.AsQueryable();

            // Counter to see if context.add method is called first
            int addReport = 0;

            SetUpDBReports(reports);
            _context.Setup(c => c.SprintReports).Returns(_mockReports.Object);
            _context.Setup(c => c.SprintReports.Add(It.IsAny<SprintReport>())).Callback(() => addReport = callCount++);
            _context.Setup(c => c.SaveChanges()).Callback(() => save = callCount++);

            // Act
            _subject.CreateReport(report);

            // Assert
            _context.Verify(c => c.SprintReports.Add(It.IsAny<SprintReport>()), Times.Once());
            _context.Verify(c => c.SaveChanges(), Times.Once());

            // Check the counters to confirm the call order.
            Assert.Equal(0, addReport);
            Assert.Equal(1, save);
        }

        #endregion

        #region Delete
        [Fact]
        public void DeleteReport_WhenReportExists_DeleteAndSaveChangesInvoked()
        {
            // Arrange
            var report = new SprintReport()
            {
                CreatedByUser = "test",
                Id = 1
            };

            var reports = new List<SprintReport> { report }.AsQueryable();

            // Counter to see if context.add method is called first
            int deleteReport = 0;

            SetUpDBReports(reports);
            _mockReports.Setup(m => m.Find(report.Id)).Returns(report);

            _context.Setup(c => c.SprintReports).Returns(_mockReports.Object);
            _context.Setup(c => c.SprintReports.Remove(It.IsAny<SprintReport>())).Callback(() => deleteReport = callCount++);
            _context.Setup(c => c.SaveChanges()).Callback(() => save = callCount++);

            // Act
            _subject.DeleteReport(1);

            // Assert
            _context.Verify(c => c.SprintReports.Remove(It.IsAny<SprintReport>()), Times.Once());
            _context.Verify(c => c.SaveChanges(), Times.Once());

            // Check the counters to confirm the call order.
            Assert.Equal(0, deleteReport);
            Assert.Equal(1, save);
        }

        [Fact]
        public void DeleteReport_WhenReportDoesNotExist_ThrowEx()
        {
            // Arrange
            var report = new SprintReport { Id = 1 };
            var reports = new List<SprintReport> { report }.AsQueryable();

            _mockReports.Setup(m => m.Find(report.Id)).Returns(report);
            SetUpDBReports(reports);

            _context.Setup(c => c.SprintReports).Returns(_mockReports.Object);

            var expected = "Sprint Report not found";

            // Act + Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => _subject.DeleteReport(2));
            Assert.Equal(expected, exception.Message);
        }

        #endregion
    }
}