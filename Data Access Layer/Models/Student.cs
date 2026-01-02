using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
   public class Student
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public float GPA { get; set; }
        public string Skills { get; set; }
        public string Interests { get; set; }
        public string Description { get; set; }
        public string TeamRole { get; set; } // Leader, Member
        public DateTime? GraduationDate { get; set; }

        public User User { get; set; }
        public Team Team { get; set; }
        public string TeamId { get; set; }
        public bool IsActive { get; set; } = true;


        public List<ProjectTask> Tasks { get; set; }

        public Student()
        {
            Id = Guid.NewGuid().ToString();
            Tasks = new List<ProjectTask>();
        }
    }
}
