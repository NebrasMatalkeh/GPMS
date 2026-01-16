using Business_Logic.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Supervisor")]
    public class SupervisorController : ControllerBase
    {
        private readonly ISupervisorService _supervisorService;

        public SupervisorController(ISupervisorService supervisorService)
        {
            _supervisorService = supervisorService;
        }

        [HttpGet("my-profile")]
        public IActionResult GetMyProfile()
        {
            var userId = User.FindFirst("id")?.Value;
            var supervisor = _supervisorService.GetByUserId(userId);
            return Ok(supervisor);
        }

        [HttpGet("teams")]
        public IActionResult GetMyTeams()
        {
            var userId = User.FindFirst("id")?.Value;
            var supervisor = _supervisorService.GetByUserId(userId);

            if (supervisor == null) return NotFound();

            var teams = _supervisorService.GetSupervisedTeams(supervisor.Id);
            return Ok(teams);
        }

        [HttpPost("assign-team")]
        public IActionResult AssignTeam(string teamId)
        {
            var userId = User.FindFirst("id")?.Value;
            var supervisor = _supervisorService.GetByUserId(userId);

            _supervisorService.AssignTeam(supervisor.Id, teamId);
            return Ok("Team assigned successfully");
        }
    }
}