using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjectTracker.Models
{
    public class Work
    {
        //Primary key
        public int WorkID { get; set; }

        
        //Foreign key forthe associated developer
        public string DeveloperID { get; set; }
        public Developer Developer { set; get; }


        //Foreign key for sprint task associated
        public int STaskID { get; set; }
        public STask STask { get; set; }

        //Work details
        public string WorkTitle { set; get; }
        public string WorkDescription { set; get; }
        public WorkStatus Status { set; get; }
        public string RejectionNote { set; get; }

        
        //file info
        public Byte[] WorkFile { get; set; }
        public string WorkFileName { get; set; }
        public string ContentType { get; set; }
    }
    public enum WorkStatus
    {
        Complete = 1,
        Pending = 2,
        Rejected=3
    }
}
