using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class DefenseSchedule
    {
        [MaxLength(450)]
        public string Id { get; set; }
        public string TeamId { get; set; }
        public DateTime DefenseDate { get; set; }
        public string Location { get; set; }
        public string Status { get; set; } // Scheduled, Completed, Cancelled
        public string Result { get; set; } // Pass, Fail, Conditional
        public string CoordinatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
      
        public List<DefenseScheduleSupervisor> DefenseSchedulesAsEvaluator { get; set; } = new();
      

        public Team Team { get; set; }
        public Coordinator Coordinator { get; set; }


    }
    public class DefenseScheduleSupervisor
    {
        public string DefenseScheduleId { get; set; }
        public DefenseSchedule DefenseSchedule { get; set; }

        public string SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; }
    }

}
