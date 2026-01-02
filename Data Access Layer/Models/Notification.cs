using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
   public class Notification
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; } // Task, Meeting, Deadline, System
        public string RelatedEntityId { get; set; }

        public User User { get; set; }

        public Notification()
        {
            Id = Guid.NewGuid().ToString();
            SentAt = DateTime.UtcNow;
            IsRead = false;
        }
    }
}
