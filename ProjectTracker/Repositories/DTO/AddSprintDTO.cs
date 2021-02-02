using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracker.Repositories.DTO
{
    public class AddSprintDTO
    {
        public int MainProjectID { get; set; }
        public string TeamLeaderID { get; set; }

        //Details of Sprint
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true,
               DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        [DisplayFormat(ApplyFormatInEditMode = true,
               DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DueDate { get; set; }


        [Display(Name = "Sprint Status")]
        public Models.SprintStatus Status { set; get; }
    }
}
