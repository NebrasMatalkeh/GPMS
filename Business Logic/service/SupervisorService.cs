using Business_Logic.Interface;
using Data_Access_Layer;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic.service
{
    public class SupervisorService : ISupervisorService
    {
        private readonly AppDbContext _context;

        public SupervisorService(AppDbContext context)
        {
            _context = context;
        }

        public Supervisor GetByUserId(string userId)
        {
            return _context.Supervisors
                .Include(s => s.SupervisedTeams)
                .FirstOrDefault(s => s.UserId == userId);
        }

        public IEnumerable<Team> GetSupervisedTeams(string supervisorId)
        {
            return _context.Teams
                .Where(t => t.SupervisorId == supervisorId)
                .Include(t => t.Members)
                .ToList();
        }

        public void AssignTeam(string supervisorId, string teamId)
        {
            var supervisor = _context.Supervisors.Find(supervisorId);
            var team = _context.Teams.Find(teamId);

            if (supervisor == null || team == null)
                throw new Exception("Supervisor or Team not found");

            if (supervisor.CurrentTeams >= supervisor.MaxTeams)
                throw new Exception("Supervisor reached max teams");

            team.SupervisorId = supervisorId;
            supervisor.CurrentTeams++;

            _context.SaveChanges();
        }

        public void UpdateMaxTeams(string supervisorId, int maxTeams)
        {
            var supervisor = _context.Supervisors.Find(supervisorId);
            if (supervisor == null) return;

            supervisor.MaxTeams = maxTeams;
            _context.SaveChanges();
        }
    }
}
