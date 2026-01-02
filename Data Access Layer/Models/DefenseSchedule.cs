using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
   public class DefenseSchedule
    {
        public string Id { get; set; }
        public string TeamId { get; set; }
        public DateTime DefenseDate { get; set; }
        public string Location { get; set; }
        public string Status { get; set; } // Scheduled, Completed, Cancelled
        public string Result { get; set; } // Pass, Fail, Conditional
        public string CoordinatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Team Team { get; set; }
        public Coordinator Coordinator { get; set; }
        public List<Supervisor> Evaluators { get; set; }

        public DefenseSchedule()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            Status = "Scheduled";
            Evaluators = new List<Supervisor>();
        }
    }
}
