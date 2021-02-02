using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Repositories;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Controllers
{
    public class AdminController : Controller
    {
        IProjectManagerRepository _ProjectManagerRepo;
        ITeamLeaderRepository _TeamLeaderRepo;
        IDeveloperRepository _DeveloperRepo;

        public AdminController(IProjectManagerRepository projectManagerRepository , ITeamLeaderRepository teamLeaderRepository, IDeveloperRepository developerRepository)
        {
            _ProjectManagerRepo = projectManagerRepository;
            _TeamLeaderRepo = teamLeaderRepository;
            _DeveloperRepo = developerRepository;
        }
        public IActionResult ShowAllProjectManagers()
        {
            return View(model: _ProjectManagerRepo.GetAllProjectManagers());
        }

        public IActionResult AddNewProjectManager()
        {
            return View();
        }
        public IActionResult InsertRecordProjectManager(AddPersonDTO addPersonDTO)
        {
            if (ModelState.IsValid)
            {
                _ProjectManagerRepo.AddNewProjectManager(addPersonDTO);
                return View("ShowAllProjectManagers", model: _ProjectManagerRepo.GetAllProjectManagers());
            }
            else
            {
                return View("AddNewProjectManager");
            }
        }

        public IActionResult EditProjectManager(string id)
        {
            return View(model: _ProjectManagerRepo.GetProjectManagerByID(id));
        }

        public IActionResult UpdateRecordProjectManager(EditPersonDTO editPersonDTO)
        {
            if (ModelState.IsValid)
            {
                _ProjectManagerRepo.UpdateProjectManager(editPersonDTO);
                return View("ShowAllProjectManagers", model: _ProjectManagerRepo.GetAllProjectManagers());
            }
            else
            {
                return View("EditProjectManager", model: _ProjectManagerRepo.GetProjectManagerByID(editPersonDTO.PersonID));
            }

        }

        public IActionResult ShowAllTeamLeaders()
        {
            return View(model: _TeamLeaderRepo.GetAllTeamLeaders());
        }

        public IActionResult AddNewTeamLeader()
        {
            return View();
        }
        public IActionResult InsertRecordTeamLeader(AddPersonDTO addPersonDTO)
        {
            if (ModelState.IsValid)
            {
                _TeamLeaderRepo.AddNewTeamLeader(addPersonDTO);
                return View("ShowAllTeamLeaders", model: _TeamLeaderRepo.GetAllTeamLeaders());
            }
            else
            {
                return View("AddNewTeamLeader");
            }
        }

        public IActionResult EditTeamLeader(string id)
        {
            return View(model: _TeamLeaderRepo.GetTeamLeaderByID(id));
        }
        public IActionResult UpdateRecordTeamLeader(EditPersonDTO editPersonDTO)
        {
            if (ModelState.IsValid)
            {
                _TeamLeaderRepo.UpdateTeamLeader(editPersonDTO);
                return View("ShowAllTeamLeaders", model: _TeamLeaderRepo.GetAllTeamLeaders());
            }
            else
            {
                return View("EditTeamLeader", model: _TeamLeaderRepo.GetTeamLeaderByID(editPersonDTO.PersonID));
            }

        }
        public IActionResult ShowAllDevelopers()
        {
            return View(model: _DeveloperRepo.GetAllDevelopers());
        }

        public IActionResult AddNewDeveloper()
        {
            return View();
        }

        public IActionResult InsertRecordDeveloper(AddPersonDTO addPersonDTO)
        {
            if (ModelState.IsValid)
            {
                _DeveloperRepo.AddNewDeveloper(addPersonDTO);
                return View("ShowAllDevelopers", model: _DeveloperRepo.GetAllDevelopers());
            }
            else
            {
                return View("AddNewDeveloper");
            }
        }
        public IActionResult EditDeveloper(string id)
        {
            return View(model: _DeveloperRepo.GetDeveloperByID(id));
        }
        public IActionResult UpdateRecordDeveloper(EditPersonDTO editPersonDTO)
        {
            if (ModelState.IsValid)
            {
                _DeveloperRepo.UpdateDeveloper(editPersonDTO);
                return View("ShowAllDevelopers", model: _DeveloperRepo.GetAllDevelopers());
            }
            else
            {
                return View("EditDeveloper", model: _DeveloperRepo.GetDeveloperByID(editPersonDTO.PersonID));
            }

        }
    }
}
