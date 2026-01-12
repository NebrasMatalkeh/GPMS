using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Business_Logic_Layer;
using Business_Logic_Layer.DTOS;
using Data_Access_Layer;
using Data_Access_Layer.Models;

public class AuthServiceTests
{
    private readonly AuthService _authService;
    private readonly AppDbContext _context;

    public AuthServiceTests()
    {
        // Setup InMemory Database
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Each test uses a separate database
            .Options;

        _context = new AppDbContext(options);
        _context.Database.EnsureCreated();

        // Setup configuration for JWT
        var configuration = new ConfigurationBuilder()
      .AddInMemoryCollection(new Dictionary<string, string>
      {
        { "Jwt:Key", "ThisIsASecretKeyThatIsAtLeast32Chars!" }, // >= 32 characters
        { "Jwt:Issuer", "TestIssuer" },
        { "Jwt:Audience", "TestAudience" }
      })
      .Build();


        _authService = new AuthService(_context, configuration);
    }

    // =========================================
    // Test: Register a new user
   
    [Fact]
    public async Task Register_ValidUser_ReturnsAuthResponse()
    {
        var registerDto = new AuthDTO.RegisterDTO
        {
            Name = "Ahmed",
            Email = "ahmed@test.com",
            Password = "Password123!",
            Role = "Student"
        };

        var result = await _authService.RegisterAsync(registerDto);

        Assert.NotNull(result);
        Assert.Equal("Ahmed", result.Name);
        Assert.False(string.IsNullOrEmpty(result.Token));
    }

    // =========================================
    // Test: Login with valid user credentials
   
    [Fact]
    public async Task Login_ValidUser_ReturnsAuthResponse()
    {
        // First, register the user
        var registerDto = new AuthDTO.RegisterDTO
        {
            Name = "Sara",
            Email = "sara@test.com",
            Password = "SecurePass456!",
            Role = "Student"
        };
        await _authService.RegisterAsync(registerDto);

        var loginDto = new AuthDTO.LoginDTO
        {
            Email = "sara@test.com",
            Password = "SecurePass456!"
        };

        var result = await _authService.LoginAsync(loginDto);

        Assert.NotNull(result);
        Assert.Equal("Sara", result.Name);
        Assert.False(string.IsNullOrEmpty(result.Token));
    }

    // =========================================
    // Test: Login fails due to invalid password
   
    [Fact]
    public async Task Login_InvalidPassword_ReturnsNull()
    {
        // First, register the user
        var registerDto = new AuthDTO.RegisterDTO
        {
            Name = "John",
            Email = "john@test.com",
            Password = "Password123!",
            Role = "Teacher"
        };
        await _authService.RegisterAsync(registerDto);

        var loginDto = new AuthDTO.LoginDTO
        {
            Email = "john@test.com",
            Password = "WrongPassword!"
        };

        var result = await _authService.LoginAsync(loginDto);

        Assert.Null(result); // Should be null because password is wrong
    }

    // =========================================
    // Test: Login fails due to non-existent user
  
    [Fact]
    public async Task Login_NonExistentUser_ReturnsNull()
    {
        var loginDto = new AuthDTO.LoginDTO
        {
            Email = "notfound@test.com",
            Password = "Password123!"
        };

        var result = await _authService.LoginAsync(loginDto);

        Assert.Null(result); // Should be null because user does not exist
    }
}
