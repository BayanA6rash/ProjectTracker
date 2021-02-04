using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Repositories;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectManagerAPIController : ControllerBase
    {
        private IProjectManagerRepository _IprojectManger;
        private IMainProjectRepository _projectRep;
        public ProjectManagerAPIController(IProjectManagerRepository IprojectManger, IMainProjectRepository projectRep)
        {
            _IprojectManger = IprojectManger;
            _projectRep = projectRep;
        }

        [HttpGet]
        public IActionResult ShowProjectManger()
        {
            var AllProjects = (_IprojectManger.GetAllProjectManagers().Select(x => new AddPersonDTO { FirstName = x.FirstName, LastName = x.LastName, Email = x.Email }));
            return new JsonResult(AllProjects);
        }

        [HttpPost]
        public  IActionResult InsertProjectManger([FromBody] AddPersonDTO opj)
        {
             _IprojectManger.AddNewProjectManager(opj);
            return new ObjectResult("Done");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProjectManger(string id, [FromBody] EditPersonDTO edit)
        {
            edit.PersonID = id;
            _IprojectManger.UpdateProjectManager(edit);
            return new ObjectResult("Done");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletProjectManger(string id)
        {
            _IprojectManger.DeleteProjectManagerByID(id);
            return new ObjectResult("Done");
        }

    }
}