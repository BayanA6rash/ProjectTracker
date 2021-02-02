using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Repositories.DTO
{
    public class EditWorkDTO
    {
        public int WorkID { get; set; }
        public int STaskID { get; set; }

        [Display(Name = "Work Status")]
        public Models.WorkStatus Status { set; get; }
        
        [Display(Name = "Rejection Note")]
        public string note { set; get; }
    }
}
