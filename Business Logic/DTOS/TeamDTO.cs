using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.DTOS
{
    public class TeamDTO
    {
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string LeaderId { get; set; }
        public List<string> Members { get; set; }
    }
}
