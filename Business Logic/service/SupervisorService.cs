using Data_Access_Layer.Models;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;

namespace Business_Logic_Layer.Services
{
    public class SupervisorService
    {
        private readonly AppDbContext _context;

        public SupervisorService(AppDbContext context)
        {
            _context = context;
        }

        // 1. جلب المشرف حسب ID
        public Supervisor GetSupervisorById(string supervisorId)
        {
            return _context.Supervisors
                .Include(s => s.SupervisedTeams)
                .FirstOrDefault(s => s.Id == supervisorId);
        }

        // 2. عرض جميع الفرق التي يشرف عليها
        public List<Team> GetSupervisedTeams(string supervisorId)
        {
            var supervisor = GetSupervisorById(supervisorId);
            return supervisor?.SupervisedTeams ?? new List<Team>();
        }

        // 3. التحقق إذا كان يمكن قبول فريق جديد
        public bool CanAcceptNewTeam(string supervisorId)
        {
            var supervisor = GetSupervisorById(supervisorId);
            return supervisor != null && supervisor.CurrentTeams < supervisor.MaxTeams;
        }

        // 4. قبول فريق
        public bool AcceptTeam(string supervisorId, string teamId)
        {
            var supervisor = GetSupervisorById(supervisorId);
            var team = _context.Teams.FirstOrDefault(t => t.Id == teamId);

            if (supervisor == null || team == null)
                return false;

            if (!CanAcceptNewTeam(supervisorId))
                return false;

            team.SupervisorId = supervisor.Id;
            supervisor.CurrentTeams++;

            _context.SaveChanges();
            return true;
        }

        // 5. رفض فريق
        public bool RejectTeam(string teamId)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == teamId);
            if (team == null)
                return false;

            team.SupervisorId = null;
            _context.SaveChanges();
            return true;
        }

        // 6. تعديل الحد الأعلى للفرق
        public bool UpdateMaxTeams(string supervisorId, int newMax)
        {
            var supervisor = GetSupervisorById(supervisorId);
            if (supervisor == null || newMax < supervisor.CurrentTeams)
                return false;

            supervisor.MaxTeams = newMax;
            _context.SaveChanges();
            return true;
        }
    }
}
