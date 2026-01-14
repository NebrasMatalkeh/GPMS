using Business_Logic_Layer.DTOS;
using Business_Logic_Layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Student,Admin")]
    public class TeamController : ControllerBase

    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        // =========================
        // Create Team
        // =========================
        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDTO dto)
        {
            var leaderId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(leaderId))
                return Unauthorized();

            var result = await _teamService.CreateTeamAsync(leaderId, dto);

            if (!result)
                return BadRequest("Failed to create team");

            return Ok("Team created successfully");
        }

        // =========================
        // Get My Team
        // =========================
        [HttpGet("my-team")]
        public async Task<IActionResult> GetMyTeam()
        {
            var studentId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(studentId))
                return Unauthorized();

            var team = await _teamService.GetTeamByStudentIdAsync(studentId);

            if (team == null)
                return NotFound("You are not assigned to any team");

            return Ok(team);
        }

        // =========================
        // Invite Student (Prototype)
        // =========================
        [HttpPost("invite")]
        public async Task<IActionResult> InviteStudent([FromQuery] string studentId)
        {
            var leaderId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(leaderId))
                return Unauthorized();

            var result = await _teamService.InviteStudentAsync(leaderId, studentId);

            if (!result)
                return BadRequest("Failed to invite student");

            return Ok("Invitation sent");
        }

        // =========================
        // Remove Student (Prototype)
        // =========================
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveStudent([FromQuery] string studentId)
        {
            var leaderId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(leaderId))
                return Unauthorized();

            var result = await _teamService.RemoveStudentAsync(leaderId, studentId);

            if (!result)
                return BadRequest("Failed to remove student");

            return Ok("Student removed from team");
        }
    }
}
