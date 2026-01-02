using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Supervisor
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ResearchInterests { get; set; }
        public int MaxTeams { get; set; }
        public int CurrentTeams { get; set; }
        public string Department { get; set; }


        public User User { get; set; }
        public List<Team> SupervisedTeams { get; set; }
       
        
        public List<DefenseScheduleSupervisor> DefenseSchedulesAsEvaluator { get; set; } = new();


      
        public List<DefenseSchedule> DefenseSchedules { get; set; } = new();   

        public Supervisor()
        {
            Id = Guid.NewGuid().ToString();
            MaxTeams = 5;
            CurrentTeams = 0;
            SupervisedTeams = new List<Team>();
         
        }
    }
}
