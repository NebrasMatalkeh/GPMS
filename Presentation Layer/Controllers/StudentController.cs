using Business_Logic_Layer.DTOS;
using Business_Logic_Layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Student,Admin,Supervisor")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirst("id")?.Value;
        var profile = await _studentService.GetStudentProfileAsync(userId);
        return Ok(profile);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(string id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateProfile([FromBody] StudentDTO dto)
    {
        var userId = User.FindFirst("id")?.Value;
        var result = await _studentService.UpdateStudentProfileAsync(userId, dto);
        if (!result) return BadRequest();
        return Ok("Profile updated successfully.");
    }

    [HttpGet("tasks")]
    public async Task<IActionResult> GetTasks()
    {
        var userId = User.FindFirst("id")?.Value;
        var tasks = await _studentService.GetStudentTasksAsync(userId);
        return Ok(tasks);
    }

    [HttpPost("submit-task")]
    public async Task<IActionResult> SubmitTask([FromQuery] string taskId, [FromBody] string submissionUrl)
    {
        var userId = User.FindFirst("id")?.Value;
        var result = await _studentService.SubmitTaskAsync(userId, taskId, submissionUrl);
        if (!result) return BadRequest();
        return Ok("Task submitted successfully.");
    }

    [HttpGet("progress")]
    public async Task<IActionResult> GetProgress()
    {
        var userId = User.FindFirst("id")?.Value;
        var progress = await _studentService.GetStudentProgressAsync(userId);
        return Ok(progress);
    }

    [HttpGet("invitations")]
    public async Task<IActionResult> GetInvitations()
    {
        var userId = User.FindFirst("id")?.Value;
        var invitations = await _studentService.GetStudentInvitationsAsync(userId);
        return Ok(invitations);
    }

    [HttpPost("accept-invitation")]
    public async Task<IActionResult> AcceptInvitation([FromQuery] string teamId)
    {
        var userId = User.FindFirst("id")?.Value;
        var result = await _studentService.AcceptTeamInvitationAsync(userId, teamId);
        if (!result) return BadRequest();
        return Ok("Invitation accepted successfully.");
    }
}
