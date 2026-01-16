using Xunit;
using Microsoft.EntityFrameworkCore;
using Business_Logic_Layer.Services;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GPMS.Testing.TeamTests
{
    public class TeamServiceTests
    {
        private readonly AppDbContext _context;
        private readonly TeamService _service;

        public TeamServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            _service = new TeamService(_context);
        }

        [Fact]
        public async Task GetTeamByStudentId_Leader_ReturnsTeam()
        {
            // Arrange
            var team = new Team
            {
                Id = "t1",
                TeamName = "Team Alpha",
                ProjectTitle = "GPMS",
                ProjectDescription = "Graduation Project",
                LeaderId = "leader1",
                SupervisorId = "sup1" // ⭐ Required
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetTeamByStudentIdAsync("leader1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Team Alpha", result.TeamName);
        }
    }
}
