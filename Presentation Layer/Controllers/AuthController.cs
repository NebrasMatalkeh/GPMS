using Business_Logic_Layer.DTOS;
using Business_Logic_Layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthDTO.RegisterDTO dto)
    {
        var result = await _authService.RegisterAsync(dto);
        if (result == null) return BadRequest("Email already exists.");
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthDTO.LoginDTO dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (result == null) return Unauthorized("Invalid credentials.");
        return Ok(result);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst("id")?.Value;
        var profile = await _authService.GetUserProfileAsync(userId);
        if (profile == null) return NotFound();
        return Ok(profile);
    }

    [Authorize]
    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] AuthDTO.UpdatUserDTO dto)
    {
        var userId = User.FindFirst("id")?.Value;
        var result = await _authService.UpdateUserProfileAsync(userId, dto);
        if (!result) return BadRequest();
        return Ok("Profile updated successfully.");
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] AuthDTO.ChangePasswordDTO dto)
    {
        var userId = User.FindFirst("id")?.Value;
        var result = await _authService.ChangePasswordAsync(userId, dto.OldPassword, dto.NewPassword);
        if (!result) return BadRequest("Old password is incorrect.");
        return Ok("Password changed successfully.");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] string email)
    {
        var result = await _authService.ResetPasswordAsync(email);
        if (!result) return NotFound("Email not found.");
        return Ok("Temporary password generated. Check console (or email).");
    }
}

