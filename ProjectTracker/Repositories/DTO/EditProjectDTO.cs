using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;

namespace ProjectTracker.Repositories.DTO
{
    public class EditProjectDTO
    {
        //primary key
        public int MainProjectID { get; set; }

        //Forign key for the team leader
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
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
        [Required]
        [Display(Name = "Project Status")]
        public ProjectStatus Status { set; get; }

    }
}
