using Xunit;
using Microsoft.EntityFrameworkCore;
using Business_Logic_Layer.Services;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using System;

namespace GPMS.Testing.StudentTests
{
    public class StudentServiceTests
    {
        private readonly AppDbContext _context;
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            _service = new StudentService(_context);
        }

        [Fact]
        public async void GetStudentProfile_ReturnsStudentDTO()
        {
            var user = new User
            {
                Id = "u1",
                Name = "Student One",
                Email = "student@test.com",
                PasswordHash = "hash",
                Role = "Student"
            };

            var team = new Team
            {
                Id = "team1",
                TeamName = "Team B",
                ProjectTitle = "GPMS",
                ProjectDescription = "Graduation Project",
                LeaderId = "u2",
                SupervisorId = "sup2"
            };

            _context.Teams.Add(team);


            var student = new Student
            {
                Id = "s2",
                UserId = "u2",
                User = user,
                GPA = 2.5f,
                Skills = "Old",
                Interests = "Old",
                Description = "Old",
                TeamId = "team1",
                TeamRole = "Member",
                Team = team 
            };


            _context.Users.Add(user);
            _context.Teams.Add(team);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var result = await _service.GetStudentProfileAsync("u1");

            Assert.NotNull(result);
            Assert.Equal("Student One", result.Name);
            Assert.Equal("Team B", result.TeamName);
        }

        [Fact]
        public async void UpdateStudentProfile_UpdatesData()
        {
            var user = new User
            {
                Id = "u2",
                Name = "Sara",
                Email = "sara@test.com",
                PasswordHash = "hash",
                Role = "Student"
            };

            var student = new Student
            {
                Id = "s2",
                UserId = "u2",
                User = user,
                GPA = 2.5f,
                Skills = "Old",
                Interests = "Old",
                Description = "Old",
                TeamId = "team1",
                TeamRole = "Member"
            };

            _context.Users.Add(user);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var result = await _service.UpdateStudentProfileAsync("u2", new()
            {
                GPA = 3.9f,
                Skills = "New Skills",
                Interests = "New Interests",
                Description = "Updated"
            });

            Assert.True(result);
        }
    }
}
