using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class MainProject
    {
        //primary key
        public int MainProjectID { get; set; }


        //Forign key for the project manager
        public string ProjectManagerID { get; set; }
        public ProjectManager ProjectManager { set; get; } 

        
        //Forign key for the team leader
        public string TeamLeaderID { get; set; }
        public TeamLeader TeamLeader { set; get; }


        //List of developers associated to this project
        public List<MainProjectDeveloper> MainProjectDevelopers { get; set; }
        
        
        //List of sprints associated to this projetc
        public List<Sprint> Sprints { set; get; }

        
        //Details of project
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public ProjectStatus Status { set; get; }
       
    }
    public enum ProjectStatus
    {
        Completed = 1,
        Pending = 2,
    }
}
