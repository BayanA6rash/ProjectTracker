using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjectTracker.Repositories.DTO
{
    public class AddWorkDTO
    {

        public int STaskID { get; set; }
        public string DeveloperID { get; set; }

        //Details of Sprint
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "File")]
        public IFormFile file { get; set; }

       

    }
}
