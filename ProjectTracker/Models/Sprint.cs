using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class Sprint
    {
        //Primary key
        public int SprintID { get; set; }


       //Forign key for the main project
        public int MainProjectID { get; set; }
        public MainProject MainProject { set; get; }


        //Forign key for the team leader
        public string TeamLeaderID { get; set; }
        public TeamLeader TeamLeader { set; get; }


        //List of sprint tasks
        public List<STask> STasks { get; set; }

        //Sprint details
        public string SprintTitle { get; set; }
        public string SprintDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public SprintStatus Status { set; get; }
    }

    public enum SprintStatus
    {
        Complete = 1,
        Pending = 2
    }
}
