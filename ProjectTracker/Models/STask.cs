using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class STask
    {
        //Primary key
        public int STaskID { set; get; }

        //Forign key for the sprint
        public Sprint Sprint { get; set; }
        public int SprintID { get; set; }

        //Forign key for the associated developer
        public Developer Developer { set; get; }
        public string DveloperID { set; get; }

        //List of works by the developer
        public List<Work> works { set; get; }

        //Sprint tasks details
        public string STaskTitle { get; set; }
        public string STaskDescription { get; set; }
        public STaskStatus Status { get; set; }

    }
    public enum STaskStatus
    {
        Complete = 1,
        Pending = 2
    }
}
