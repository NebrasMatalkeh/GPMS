using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Business_Logic_Layer.DTOS;

namespace Business_Logic_Layer.Interface
{
    public interface ITeamService
    {
        Task<bool> CreateTeamAsync(string leaderId, CreateTeamDTO dto);
        Task<TeamDTO?> GetTeamByStudentIdAsync(string studentId);
        Task<bool> InviteStudentAsync(string leaderId, string studentId);
        Task<bool> RemoveStudentAsync(string leaderId, string studentId);
    }
}
