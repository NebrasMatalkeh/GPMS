using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class ProjectTask
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; } // Pending, In Progress, Completed, Overdue
        public string DeliverableUrl { get; set; }
        public string TeamId { get; set; }
        public string StudentId { get; set; }
        public string SupervisorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Team Team { get; set; }
        public Student Student { get; set; }
        public Supervisor Supervisor { get; set; }
        public DateTime? SubmittedAt { get; set; }


        public ProjectTask()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            Status = "Pending";
        }
    }
}
