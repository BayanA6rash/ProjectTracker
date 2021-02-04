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
    public class TeamLeaderController : Controller
    {
        private IMainProjectRepository _MainProjectRepo;
        private IDeveloperRepository _DeveloperRepo;
        private ITeamLeaderRepository _TeamLeaderRepo;
        private ISprintRepository _SprintRepo;
        private ISTaskRepository _STaskRepo;
        private IWorkRepository _WorkRepo;
        public TeamLeaderController(IWorkRepository workRepository,ISTaskRepository sTaskRepository,ISprintRepository sprintRepository ,IMainProjectRepository mainProjectRepository, IDeveloperRepository developerRepository , ITeamLeaderRepository teamLeaderRepository)
        {
            _MainProjectRepo = mainProjectRepository;
            _DeveloperRepo = developerRepository;
            _TeamLeaderRepo = teamLeaderRepository;
            _SprintRepo = sprintRepository;
            _STaskRepo = sTaskRepository;
            _WorkRepo = workRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        //Main Project Privillages
        public IActionResult ShowAllMainProjects()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View(model: _MainProjectRepo.GetAllMainProjectsByTeamLeaderID(userID));
        }


        //Sprints Privillages
        public IActionResult ShowAllProjectSprints(int id)
        {
            ViewBag.ProjectID = id;
            return View(model: _SprintRepo.GetSprintByProjectID(id) ); 
        }

        public IActionResult EditSprint(int id)
        {
            return View(model: _SprintRepo.GetSprintBySprintID(id));
        }

        public IActionResult UpdateRecordSprint(EditSprintDTO editSprintDTO)
        {
            if (ModelState.IsValid)
            {
                _SprintRepo.UpdateSprint(editSprintDTO);
                return View("ShowAllProjectSprints", model: _SprintRepo.GetSprintByProjectID(editSprintDTO.MainProjectID));
            }
            else
            {
                return View("EditSprint", model: _SprintRepo.GetSprintBySprintID(editSprintDTO.SprintID));
            }
        }

        public IActionResult AddNewSprint(int id)
        {
            AddSprintDTO addSprintDTO = new AddSprintDTO()
            {
                MainProjectID = id,
                TeamLeaderID = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            return View(model: addSprintDTO);
        }

        public IActionResult InsertRecordSprint(AddSprintDTO addSprintDTO)
        {
            if (ModelState.IsValid)
            {
                _SprintRepo.AddSprint(addSprintDTO);
                return View("ShowAllProjectSprints", model: _SprintRepo.GetSprintByProjectID(addSprintDTO.MainProjectID));
            }
            else
            {
                return View("AddNewSprint", addSprintDTO.MainProjectID);
            }
        }

        //STask privillages
        public IActionResult ShowAllSTasks(int id)
        {
            ViewBag.SprintID = id;
            ViewBag.model = _STaskRepo.GetSTaskBySprintID(id);
            return View();
        }

        public IActionResult EditSTask(int id)
        {
            var sprintID = _STaskRepo.GetSprintIDBySTaskID(id);
            var projectID = _SprintRepo.GetProjectIDBySprintID(sprintID);
            ViewBag.Developers = _DeveloperRepo.GetDevelopersByProjectID(projectID);

            return View(model: _STaskRepo.GetSTaskBySTaskID(id));
        }

        public IActionResult UpdateRecordSTask(EditSTaskDTO editSTaskDTO)
        {
            if (ModelState.IsValid)
            {
                _STaskRepo.UpdateStask(editSTaskDTO);
                return View("ShowAllSTasks");
            }
            else
            {
                return View("EditSTask", model: _STaskRepo.GetSTaskBySTaskID(editSTaskDTO.STaskID));
            }
        }

        public IActionResult AddNewSTask(int id)
        {
            var projectID = _SprintRepo.GetProjectIDBySprintID(id);
            ViewBag.Developers = _DeveloperRepo.GetDevelopersByProjectID(projectID);
            var sprintID = id;

            AddSTaskDTO addSTaskDTO = new AddSTaskDTO()
            {
                SprintID = sprintID
            };
            return View(model: addSTaskDTO);
        }

        public IActionResult InsertRecordSTask(AddSTaskDTO addSTaskDTO)
        {
            if (ModelState.IsValid)
            {
                _STaskRepo.AddSTask(addSTaskDTO);
                return View("ShowAllSTasks");
            }
            else
            {
                return View("AddNewSTask", addSTaskDTO.SprintID);
            }

        }

        //works privillages

        public IActionResult ShowAllWorks(int id)
        {
            ViewBag.WorkID = id;
            return View(model: _WorkRepo.GetWorkBySTaskID(id));
        }

        public IActionResult EditWork(int id)
        {
            return View(model: _WorkRepo.GetWorkByWorkID(id));
        }

        public IActionResult UpdateRecordWork(EditWorkDTO editWorkDTO)
        {
            if (ModelState.IsValid)
            {
                _WorkRepo.UpdateWork(editWorkDTO);
                return View("ShowAllWorks", model: _WorkRepo.GetWorkBySTaskID(editWorkDTO.STaskID));
            }
            else
            {
                return View("EditWork", model: _WorkRepo.GetWorkByWorkID(editWorkDTO.WorkID));
            }
        }

        public FileStreamResult GetFile(int id)
        {
            var work = _WorkRepo.GetWork(id);
            Stream stream = new MemoryStream(work.WorkFile); // from bytes to stream
            return new FileStreamResult(stream, work.ContentType);
        }
    }
}
