using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class TeamLeader:UserInfo
    {
        //List of sprints created by the team leader
        public List<Sprint> Sprints { get; set; }


        //List of Projects associated to the team leader
        public List<MainProject> MainProjects { get; set; }
    }
}
