using Business_Logic_Layer.DTOS;
using Business_Logic_Layer.Services;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Xunit;
using static Business_Logic_Layer.DTOS.AuthDTO;

namespace GPMS.Testing.AuthTests
{
    public class AuthServiceTests
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            var config = new ConfigurationBuilder()
      .AddInMemoryCollection(new Dictionary<string, string>
      {
        { "Jwt:Key", "THIS_IS_A_VERY_LONG_TEST_SECRET_KEY_32_BYTES_MIN" },
        { "Jwt:Issuer", "TestIssuer" },
        { "Jwt:Audience", "TestAudience" }
      })
      .Build();


            _authService = new AuthService(_context, config);
        }

        [Fact]
        public async void Register_NewUser_ReturnsUser()
        {
            var dto = new RegisterDTO
            {
                Name = "Ahmad",
                Email = "ahmad@test.com",
                Password = "123456",
                Role = "Student" 
            };



            var result = await _authService.RegisterAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("ahmad@test.com", result.Email);
        }

        [Fact]
        public async void Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var user = new User
            {
                Id = "u1",
                Name = "Test User",
                Email = "login@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Student"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var dto = new LoginDTO
            {
                Email = "login@test.com",
                Password = "123456"
            };

            // Act
            var token = await _authService.LoginAsync(dto);

            // Assert
            Assert.NotNull(token);
        }

        [Fact]
        public async void Login_WrongPassword_ReturnsNull()
        {
            // Arrange
            var user = new User
            {
                Id = "u2",
                Name = "Wrong Pass User", // ⭐ مهم
                Email = "fail@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("correct"),
                Role = "Student"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var dto = new LoginDTO
            {
                Email = "fail@test.com",
                Password = "wrong"
            };

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            Assert.Null(result);
        }
    }
}
