
using Business_Logic_Layer.DTOS;
using Business_Logic_Layer.Interface;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StudentDTO> GetStudentProfileAsync(string userId)
    {
        return await _context.Students
            .Where(s => s.UserId == userId)
            .Select(s => new StudentDTO
            {
                Id = s.Id,
                UserId = s.UserId,
                Name = s.User.Name,
                Email = s.User.Email,
                GPA = s.GPA,
                Skills = s.Skills,
                Interests = s.Interests,
                TeamId = s.TeamId,
                TeamName = s.Team != null ? s.Team.TeamName : null,


            }).FirstOrDefaultAsync();
    }

    public async Task<StudentDTO> GetStudentByIdAsync(string studentId)
    {
        return await _context.Students
            .Where(s => s.Id == studentId)
            .Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.User.Name,
                Email = s.User.Email,
                GPA = s.GPA
            }).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateStudentProfileAsync(string userId, StudentDTO dto)
    {
        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (student == null) return false;

        student.GPA = dto.GPA;
        student.Skills = dto.Skills;
        student.Interests = dto.Interests;
        student.Description = dto.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateStudentStatusAsync(string studentId, bool isActive)
    {
        var student = await _context.Students.FindAsync(studentId);
        if (student == null) return false;

        student.IsActive = isActive;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<StudentDTO>> GetAllStudentsAsync()
    {
        return await _context.Students.Select(s => new StudentDTO
        {
            Id = s.Id,
            Name = s.User.Name,
            Email = s.User.Email,
            GPA = s.GPA
        }).ToListAsync();
    }

    public async Task<List<StudentDTO>> GetUngroupedStudentsAsync()
    {
        return await _context.Students
            .Where(s => s.TeamId == null)
            .Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.User.Name,
                GPA = s.GPA
            }).ToListAsync();
    }

    public async Task<List<StudentDTO>> SearchStudentsAsync(StudentDTO searchDto)
    {
        var query = _context.Students.AsQueryable();

        if (searchDto.MinGPA.HasValue)
            query = query.Where(s => s.GPA >= searchDto.MinGPA);

        if (searchDto.MaxGPA.HasValue)
            query = query.Where(s => s.GPA <= searchDto.MaxGPA);

        return await query.Select(s => new StudentDTO
        {
            Id = s.Id,
            Name = s.User.Name,
            GPA = s.GPA
        }).ToListAsync();
    }

    public async Task<object> GetStudentDashboardAsync(string userId)
    {
        return new
        {
            TasksCount = await _context.ProjectTasks.CountAsync(),
            Invitations = await GetStudentInvitationsAsync(userId)
        };
    }

    public async Task<List<StudentTaskDTO>> GetStudentTasksAsync(string userId)
    {
        return await _context.ProjectTasks
            .Where(t => t.Student.UserId == userId)
            .Select(t => new StudentTaskDTO
            {
                TaskId = t.Id,
                Title = t.Title,
                Status = t.Status,
                Deadline = t.Deadline,
                IsOverdue = t.Deadline < DateTime.UtcNow
            }).ToListAsync();
    }

    public async Task<bool> SubmitTaskAsync(string userId, string taskId, string submissionUrl)
    {
        var task = await _context.ProjectTasks.FindAsync(taskId);
        if (task == null) return false;

        task.DeliverableUrl = submissionUrl;
        task.Status = "Submitted";
        task.SubmittedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<StudentInvitationDTO>> GetStudentInvitationsAsync(string userId)
    {
        return new List<StudentInvitationDTO>();
    }

    public async Task<bool> AcceptTeamInvitationAsync(string userId, string teamId)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null) return false;

        student.TeamId = teamId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<StudentProgressDTO> GetStudentProgressAsync(string userId)
    {
        return new StudentProgressDTO
        {
            StudentName = "Student",
            TaskProgress = new List<StudentTaskProgressDTO>()
        };
    }
}
