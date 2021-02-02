using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class ProjectManager:UserInfo
    {
        //List of projects for the project manager
        public List<MainProject> MainProjects { get; set; }
    }
}
