using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Interface
{
    public interface ISupervisorService
    {
        Supervisor GetByUserId(string userId);
        IEnumerable<Team> GetSupervisedTeams(string supervisorId);
        void AssignTeam(string supervisorId, string teamId);
        void UpdateMaxTeams(string supervisorId, int maxTeams);
    }
}
