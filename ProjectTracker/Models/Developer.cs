using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class Developer:UserInfo
    {
        //List of projects associated
        public ICollection<MainProjectDeveloper> MainProjects {get; set; }

        //List of tasts associated
        public ICollection<STask> STasks { get; set; }

        //List of works associated
        public ICollection<Work> Works { get; set; }
    }
}
