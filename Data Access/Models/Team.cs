using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data_Access_Layer.Models
{
    public class Team
    {
        // =========================
        // Basic Info
        // =========================
        public string Id { get; set; }
        public string TeamName { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }

        // Proposal | Progress | Final
        public string ProjectPhase { get; set; }

        // =========================
        // Relations (FKs)
        // =========================
        public string LeaderId { get; set; }
        public string? SupervisorId { get; set; }

        // =========================
        // Audit
        // =========================
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // =========================
        // Navigation Properties
        // =========================
        public Student Leader { get; set; }
        public Supervisor? Supervisor { get; set; }

        public List<Student> Members { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public List<Meeting> Meetings { get; set; }

        public DefenseSchedule? DefenseSchedule { get; set; }

        // =========================
        // Constructor
        // =========================
        public Team()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            ProjectPhase = "Proposal";

            Members = new List<Student>();
            Tasks = new List<ProjectTask>();
            Meetings = new List<Meeting>();
        }
    }
}
