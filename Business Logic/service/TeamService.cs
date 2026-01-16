using Business_Logic_Layer.DTOS;
using Business_Logic_Layer.Interface;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTeamAsync(string leaderId, CreateTeamDTO dto)
        {
            var team = new Team
            {
                TeamName = dto.TeamName,
                ProjectTitle = dto.ProjectTitle,
                ProjectDescription = dto.ProjectDescription,
                LeaderId = leaderId
            };

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TeamDTO?> GetTeamByStudentIdAsync(string studentId)
        {
            var team = await _context.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t =>
                    t.LeaderId == studentId ||
                    t.Members.Any(m => m.Id == studentId));

            if (team == null) return null;

            return new TeamDTO
            {
                Id = team.Id,
                TeamName = team.TeamName,
                ProjectTitle = team.ProjectTitle,
                ProjectPhase = team.ProjectPhase,
                LeaderId = team.LeaderId,
                MemberIds = team.Members.Select(m => m.Id).ToList()
            };
        }

        public async Task<bool> InviteStudentAsync(string leaderId, string studentId)
        {
            // Prototype logic

            return true;
        }

        public async Task<bool> RemoveStudentAsync(string leaderId, string studentId)
        {
            // Prototype logic
            return true;
        }
    }
}
