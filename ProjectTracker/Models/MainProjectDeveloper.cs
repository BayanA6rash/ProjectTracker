using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Models
{
    public class MainProjectDeveloper
    {
        //Relation table between projects and developers (M2M)
        public string DeveloperID { get; set; }
        public int MainProjectID { get; set; }

        public Developer Developer { set; get; }
        public MainProject MainProject { set; get; }
    }
}
