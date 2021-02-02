using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;

namespace ProjectTracker.Repositories.DTO
{
    public class AddProjectDTO
    {
        //ProjectManagerID 
      
        [Display(Name = "ProjectManagerID")]
        public string ProjectManagerID { get; set; }

        //TeamLeader
        [Required]
        [Display(Name = "TeamLeader")]
        public string TeamLeaderID { get; set; }

        //List of developers associated to this project
        [Required]
        [Display(Name = "Developers List")]
        public List<string> Developers { get; set; }

        //Details of project
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
        
        
        [Display(Name = "Project Status")]
        public ProjectStatus Status { set; get; }
    }
}
