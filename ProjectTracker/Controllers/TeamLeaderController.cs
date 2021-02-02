﻿using System;
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
                return View("ShowAllSprints", model: _SprintRepo.GetSprintByProjectID(editSprintDTO.MainProjectID));
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

                return View("ShowAllSprints", model: _SprintRepo.GetSprintByProjectID(addSprintDTO.MainProjectID));
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
            return View(model: _STaskRepo.GetSTaskBySprintID(id));
        }

        public IActionResult EditSTask(int id)
        {
            return View(model: _STaskRepo.GetSTaskBySTaskID(id));
        }

        public IActionResult UpdateRecordSTask(EditSTaskDTO editSTaskDTO)
        {
            if (ModelState.IsValid)
            {
                _STaskRepo.UpdateStask(editSTaskDTO);
                return View("ShowAllSTasks", model: _STaskRepo.GetSTaskBySprintID(editSTaskDTO.SprintID));
            }
            else
            {
                return View("EditSTask", model: _STaskRepo.GetSTaskBySTaskID(editSTaskDTO.STaskID));
            }
        }

        public IActionResult AddNewSTask(int id)
        {
            AddSTaskDTO addSTaskDTO = new AddSTaskDTO()
            {
                SprintID = id,
            };
            return View(model: addSTaskDTO);
        }

        public IActionResult InsertRecordSTask(AddSTaskDTO addSTaskDTO)
        {
            if (ModelState.IsValid)
            {
                _STaskRepo.AddSTask(addSTaskDTO);

                return View("ShowAllSTasks", model: _STaskRepo.GetSTaskBySprintID(addSTaskDTO.SprintID));
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

        //public IActionResult EditMainProject(int id)
        //{
        //    return View(model: _MainProjectRepo.GetMainProjectByProjectID(id));
        //}

        //public IActionResult UpdateRecordMainProject(EditProjectDTO editProjectDTO)
        //{
        //    var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    if (ModelState.IsValid)
        //    {
        //        _MainProjectRepo.UpdateProject(editProjectDTO);
        //        return View("ShowAllMainProjects", model: _MainProjectRepo.GetAllMainProjectsByTeamLeaderID(userID));
        //    }
        //    else
        //    {
        //        return View("EditMainProject", model: _MainProjectRepo.GetMainProjectByProjectID(editProjectDTO.MainProjectID));
        //    }
        //}

        //public IActionResult AddNewMainProject()
        //{
        //    ViewBag.Developers = _DeveloperRepo.GetAllDevelopers().ToList();
        //    ViewBag.TeamLeaders = _TeamLeaderRepo.GetAllTeamLeaders().ToList();
        //    return View();
        //}

        //public IActionResult InsertRecordMainProject(AddProjectDTO addProjectDTO)
        //{
        //    ViewBag.Developers = _DeveloperRepo.GetAllDevelopers().ToList();
        //    ViewBag.TeamLeaders = _TeamLeaderRepo.GetAllTeamLeaders().ToList();

        //    if (ModelState.IsValid)
        //    {
        //        addProjectDTO.ProjectManagerID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        MainProjectRepo.AddMainProject(addProjectDTO);

        //        return View("ShowAllMainProjects", model: MainProjectRepo.GetAllProjectsByUserID(addProjectDTO.ProjectManagerID));
        //    }
        //    else
        //    {
        //        return View("AddNewMainProject");
        //    }
        //}
    }
}