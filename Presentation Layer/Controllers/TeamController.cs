using Business_Logic_Layer.DTOS;
using Business_Logic_Layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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

    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDTO dto)
    {
        var userId = User.FindFirst("id")?.Value;
        var result = await _teamService.CreateTeamAsync(userId, dto);
        if (!result) return BadRequest();
        return Ok("Team created successfully.");
    }

    [HttpGet("my-team")]
    public async Task<IActionResult> GetMyTeam()
    {
        var userId = User.FindFirst("id")?.Value;
        var team = await _teamService.GetTeamByStudentIdAsync(userId);
        if (team == null) return NotFound();
        return Ok(team);
    }

    [HttpPost("invite")]
    public async Task<IActionResult> InviteStudent([FromQuery] string studentId)
    {
        var userId = User.FindFirst("id")?.Value;
        var result = await _teamService.InviteStudentAsync(userId, studentId);
        if (!result) return BadRequest();
        return Ok("Invitation sent.");
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveStudent([FromQuery] string studentId)
    {
        var userId = User.FindFirst("id")?.Value;
        var result = await _teamService.RemoveStudentAsync(userId, studentId);
        if (!result) return BadRequest();
        return Ok("Student removed.");
    }
}
