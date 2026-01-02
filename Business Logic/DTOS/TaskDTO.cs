using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.DTOS
{
    public class TaskDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string DeliverableUrl { get; set; }
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DaysUntilDeadline { get; set; }
        public bool IsOverdue { get; set; }
    }

    public class CreateTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string TeamId { get; set; }
        public string StudentId { get; set; }
    }

    public class UpdateTaskDTO
    {
        public string Status { get; set; }
        public string DeliverableUrl { get; set; }
    }

}
