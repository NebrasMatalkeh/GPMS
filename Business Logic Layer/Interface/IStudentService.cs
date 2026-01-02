using Business_Logic_Layer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interface
{
   public interface IStudentService
    {
        Task<StudentDTO> GetStudentProfileAsync(string userId);
        Task<StudentDTO> GetStudentByIdAsync(string studentId);
        Task<bool> UpdateStudentProfileAsync(string userId, StudentDTO profileDto);
        Task<bool> UpdateStudentStatusAsync(string studentId, bool isActive);

        Task<List<StudentDTO>> GetAllStudentsAsync();
        Task<List<StudentDTO>> GetUngroupedStudentsAsync();
        Task<List<StudentDTO>> SearchStudentsAsync(StudentDTO searchDto);

        Task<object> GetStudentDashboardAsync(string userId);
        Task<List<StudentTaskDTO>> GetStudentTasksAsync(string userId);
        Task<bool> SubmitTaskAsync(string userId, string taskId, string submissionUrl);

        Task<List<StudentInvitationDTO>> GetStudentInvitationsAsync(string userId);
        Task<bool> AcceptTeamInvitationAsync(string userId, string teamId);

        Task<StudentProgressDTO> GetStudentProgressAsync(string userId);
    }
}
