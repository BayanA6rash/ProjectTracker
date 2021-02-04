using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Repositories;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Controllers
{
    [Authorize(Roles = "PROJECTMANAGER")]

    public class ProjectManagerController : Controller
    {
        IMainProjectRepository _MainProjectRepo;
        IDeveloperRepository _DeveloperRepo;
        ITeamLeaderRepository _TeamLeaderRepo;
        ISprintRepository _SprintRepo;
        ISTaskRepository _STaskRepo;
        IWorkRepository _WorkRepo;
        public ProjectManagerController(IWorkRepository workRepository,ISTaskRepository sTaskRepository,ISprintRepository sprintRepository,ITeamLeaderRepository teamLeaderRepository, IDeveloperRepository developerRepository,IMainProjectRepository mainProjectRepository)
        {
            _MainProjectRepo = mainProjectRepository;
            _TeamLeaderRepo = teamLeaderRepository;
            _DeveloperRepo = developerRepository;
            _SprintRepo = sprintRepository;
            _WorkRepo = workRepository;
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

        public IActionResult ShowAllSprints(int id)
        {
            return View(model: _SprintRepo.GetSprintByProjectID(id));
        }

        public IActionResult ShowAllSTasks(int id)
        {
            ViewBag.model = _STaskRepo.GetSTaskBySprintID(id);
            return View();
        }

        public IActionResult ShowAllWorks(int id)
        {
            return View(model: _WorkRepo.GetWorkBySTaskID(id));
        }
        public FileStreamResult GetFile(int id)
        {
            var work = _WorkRepo.GetWork(id);
            Stream stream = new MemoryStream(work.WorkFile); // from bytes to stream
            return new FileStreamResult(stream, work.ContentType);
        }

    }
}
