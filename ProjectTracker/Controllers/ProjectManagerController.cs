using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Repositories;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Controllers
{
    public class ProjectManagerController : Controller
    {
        IMainProjectRepository _MainProjectRepo;
        IDeveloperRepository _DeveloperRepo;
        ITeamLeaderRepository _TeamLeaderRepo;

        public ProjectManagerController(ITeamLeaderRepository teamLeaderRepository, IDeveloperRepository developerRepository,IMainProjectRepository mainProjectRepository)
        {
            _MainProjectRepo = mainProjectRepository;
            _TeamLeaderRepo = teamLeaderRepository;
            _DeveloperRepo = developerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowAllMainProjects()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(model: _MainProjectRepo.GetAllMainProjectByProjectManagerID(userID));
        }

        public IActionResult EditMainProject(int id)
        {
            ViewBag.Developers = _DeveloperRepo.GetAllDevelopers().ToList();
            ViewBag.TeamLeaders = _TeamLeaderRepo.GetAllTeamLeaders().ToList();
            return View(model: _MainProjectRepo.GetMainProjectByProjectID(id));
        }

        public IActionResult UpdateRecordMainProject(EditProjectDTO editProjectDTO)
        {
            ViewBag.Developers = _DeveloperRepo.GetAllDevelopers().ToList();
            ViewBag.TeamLeaders = _TeamLeaderRepo.GetAllTeamLeaders().ToList();
            var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                _MainProjectRepo.UpdateProject(editProjectDTO);
                return View("ShowAllMainProjects", model: _MainProjectRepo.GetAllMainProjectByProjectManagerID(userID));
            }
            else
            {
                return View("EditMainProject", model: _MainProjectRepo.GetMainProjectByProjectID(editProjectDTO.MainProjectID));
            }
        }

        public IActionResult AddNewMainProject()
        {
            ViewBag.Developers = _DeveloperRepo.GetAllDevelopers().ToList();
            ViewBag.TeamLeaders = _TeamLeaderRepo.GetAllTeamLeaders().ToList();
            return View();
        }

        public IActionResult InsertRecordMainProject(AddProjectDTO addProjectDTO)
        {
            ViewBag.Developers = _DeveloperRepo.GetAllDevelopers().ToList();
            ViewBag.TeamLeaders = _TeamLeaderRepo.GetAllTeamLeaders().ToList();

            if (ModelState.IsValid)
            {
                addProjectDTO.ProjectManagerID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _MainProjectRepo.AddMainProject(addProjectDTO);

                return View("ShowAllMainProjects", model: _MainProjectRepo.GetAllMainProjectByProjectManagerID(addProjectDTO.ProjectManagerID));
            }
            else
            {
                return View("AddNewMainProject");
            }
        }

    }
}
