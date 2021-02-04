using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectTracker.Models;
using ProjectTracker.Repositories;

namespace ProjectTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMainProjectRepository _MainProjectRepo;
        public HomeController(IMainProjectRepository mainProjectRepository,ILogger<HomeController> logger)
        {
            _logger = logger;
            _MainProjectRepo = mainProjectRepository;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("ADMIN"))
            {
                return RedirectToAction("AddNewProjectManager",  "Admin");
            }
            else if (User.IsInRole("PROJECTMANAGER"))
            {
                return RedirectToAction("ShowAllMainProjects", "ProjectManager");
            }
            else if (User.IsInRole("TEAMLEADER"))
            {
                return RedirectToAction("ShowAllMainProjects", "TeamLeader");
            }
            else if (User.IsInRole("DEVELOPER"))
            {
                return RedirectToAction("ShowAllMainProjects", "Developer");
            }
            else
            {
                return LocalRedirect("/Identity/Account/Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
