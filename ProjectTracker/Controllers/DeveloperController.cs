using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Repositories;

namespace ProjectTracker.Controllers
{
    public class DeveloperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        IMainProjectRepository _MainProjectRepo;
        IDeveloperRepository _DeveloperRepo;
        ITeamLeaderRepository _TeamLeaderRepo;
        ISprintRepository _SprintRepo;
        ISTaskRepository _STaskRepo;
        IWorkRepository _WorkRepo;
        public DeveloperController(IWorkRepository workRepository, ISTaskRepository sTaskRepository, ISprintRepository sprintRepository, ITeamLeaderRepository teamLeaderRepository, IDeveloperRepository developerRepository, IMainProjectRepository mainProjectRepository)
        {
            _MainProjectRepo = mainProjectRepository;
            _TeamLeaderRepo = teamLeaderRepository;
            _DeveloperRepo = developerRepository;
            _SprintRepo = sprintRepository;
            _WorkRepo = workRepository;
        }

        public  IActionResult ShowAllMainProjects()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(model: _MainProjectRepo.GetAllProjectsByUserID(userID));
        }

        public IActionResult ShowAllSprints(int id)
        {
            return View(model: _SprintRepo.GetSprintByProjectID(id));
        }

        public  IActionResult ShowAllSTasks(int id)
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
