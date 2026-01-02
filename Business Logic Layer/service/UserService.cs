using Business_Logic_Layer.DTOS;
using Business_Logic_Layer.Interface;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using static Business_Logic_Layer.DTOS.AuthDTO;


public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public UserService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    // ================= JWT GENERATOR =================
    private string GenerateJwtToken(User user)
    {
        var jwt = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt["Key"])
        );

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(jwt["DurationInMinutes"])
            ),
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // ================= REGISTER =================
    public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = registerDto.Name,
            Email = registerDto.Email,
            Role = registerDto.Role,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = GenerateJwtToken(user);

        return new AuthResponseDTO
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(2)
        };
    }

    // ================= LOGIN =================
    public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            return null;

        var token = GenerateJwtToken(user);

        return new AuthResponseDTO
        {
            UserId = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(2)
        };
    }

    // ================= USER =================
    public async Task<UserDTO> GetUserByIdAsync(string userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateUserAsync(string userId, UpdatUserDTO dto)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Name = dto.Name ?? user.Name;
        user.Email = dto.Email ?? user.Email;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordDTO dto)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash))
            return false;

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<object> GetUserProfileAsync(string userId)
    {
        return await GetUserByIdAsync(userId);
    }

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        return await _context.Users.Select(u => new UserDTO
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Role = u.Role,
            CreatedAt = u.CreatedAt
        }).ToListAsync();
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
