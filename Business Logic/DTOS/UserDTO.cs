using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.DTOS
{
   public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class StudentDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public float GPA { get; set; }
        public float ? MinGPA { get; set; }
        public float ? MaxGPA { get; set; }
        public string Skills { get; set; }
        public string Interests { get; set; }
        public string Description { get; set; }
        public string TeamRole { get; set; }
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public bool ? HasTram { get; set; }
        public string SortBy{ get; set; }
        public bool SortDescending { get; set; }

        public DateTime? GraduationDate { get; set; }

    }
    public class StudentTaskDTO
    {
        public string TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string SupervisoreName { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public string SubmissionUrl { get; set; }
        public string Feedback { get; set; }
        public decimal? Score { get; set; }
        public int DaysUntilDeadline { get; set; }
        public bool IsOverdue { get; set; }

    
    }

    public class StudentInvitationDTO
    {
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string InviterName { get; set; }
        public DateTime InvitedAt { get; set; }
        public string Status { get; set; }
    }

  
    public class StudentProgressDTO
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string ProjectTitle { get; set; }
        public string CurrentPhase { get; set; }
        public List<StudentTaskProgressDTO> TaskProgress { get; set; }
    }

    public class StudentTaskProgressDTO
    {
        public string TaskId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsOverdue { get; set; }
    }

    //public class SupervisorDTO
    //{
    //    public string Id { get; set; }
    //    public string UserId { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public string ResearchInterests { get; set; }
    //    public int MaxTeams { get; set; }
    //    public int CurrentTeams { get; set; }
    //    public string Department { get; set; }
    //    public int AvailableSlots { get; set; }
    //}

    //public class CoordinatorDTO
    //{
    //    public string Id { get; set; }
    //    public string UserId { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public string Department { get; set; }
    //    public string AcademicYear { get; set; }
    //}
}
