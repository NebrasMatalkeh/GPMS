using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.DTOS
{
    public class CreateTeamDTO
    {
        public string TeamName { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
    }
}

