using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
   public class Meeting
    {
        public string Id { get; set; }
        public DateTime ProposedDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string Status { get; set; } // Requested, Approved, Rejected, Completed
        public string Notes { get; set; }
        public string MeetingType { get; set; } // Regular, Defense, Emergency
        public string TeamId { get; set; }
        public string SupervisorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Team Team { get; set; }
        public Supervisor Supervisor { get; set; }

        public Meeting()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            Status = "Requested";
        }
    }
}
