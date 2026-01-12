using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupervisorController : ControllerBase
    {
        private readonly SupervisorService _supervisorService;

        public SupervisorController(SupervisorService supervisorService)
        {
            _supervisorService = supervisorService;
        }

        // عرض بيانات المشرف
        [HttpGet("{id}")]
        public IActionResult GetSupervisor(string id)
        {
            var supervisor = _supervisorService.GetSupervisorById(id);
            if (supervisor == null)
                return NotFound();

            return Ok(supervisor);
        }

        // عرض الفرق المشرف عليها
        [HttpGet("{id}/teams")]
        public IActionResult GetTeams(string id)
        {
            return Ok(_supervisorService.GetSupervisedTeams(id));
        }

        // قبول فريق
        [HttpPost("{supervisorId}/accept/{teamId}")]
        public IActionResult AcceptTeam(string supervisorId, string teamId)
        {
            var result = _supervisorService.AcceptTeam(supervisorId, teamId);
            if (!result)
                return BadRequest("Cannot accept this team");

            return Ok("Team accepted");
        }

        // رفض فريق
        [HttpPost("reject/{teamId}")]
        public IActionResult RejectTeam(string teamId)
        {
            var result = _supervisorService.RejectTeam(teamId);
            if (!result)
                return BadRequest("Team not found");

            return Ok("Team rejected");
        }

        // تعديل الحد الأعلى للفرق
        [HttpPut("{id}/max-teams")]
        public IActionResult UpdateMaxTeams(string id, [FromBody] int newMax)
        {
            var result = _supervisorService.UpdateMaxTeams(id, newMax);
            if (!result)
                return BadRequest("Invalid max teams");

            return Ok("Max teams updated");
        }
    }
}
