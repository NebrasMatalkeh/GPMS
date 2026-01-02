using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
   public class Coordinator
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Department { get; set; }
        public string AcademicYear { get; set; }

        public User User { get; set; }
        public List<DefenseSchedule> ManagedDefenses { get; set; }
       

        public Coordinator()
        {
            Id = Guid.NewGuid().ToString();
            ManagedDefenses = new List<DefenseSchedule>();
        }
    }
}
