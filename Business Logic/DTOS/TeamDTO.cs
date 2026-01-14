using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.DTOS
{
    public class TeamDTO
    {
        public string Id { get; set; }
        public string TeamName { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectPhase { get; set; }

        public string LeaderId { get; set; }
        public List<string> MemberIds { get; set; }
    }
}

