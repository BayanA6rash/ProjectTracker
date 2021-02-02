using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Repositories.DTO
{
    public class EditSTaskDTO
    {
        public int STaskID { get; set; }
    public int SprintID { get; set; }

    [Required]
    [Display(Name = "Developer Name")]
    public string DeveloperID { get; set; }
    //Details of Sprint task
    [Required]
    [Display(Name = "Title")]
    public string Title { get; set; }

    [Required]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Display(Name = "Sprint Task Status")]
    public Models.STaskStatus Status { set; get; }
}
}
