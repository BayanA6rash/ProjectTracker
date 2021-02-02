using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ProjectTracker.Data;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;

namespace ProjectTracker.Repositories
{
    public class MainProjectRepository : IMainProjectRepository
    {
        private ApplicationDbContext context;
        public MainProjectRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void AddMainProject(AddProjectDTO addProjectDTO)
        {
            MainProject project = new MainProject()
            {
                Title = addProjectDTO.Title,
                Description = addProjectDTO.Description,
                StartDate = addProjectDTO.StartDate,
                DueDate = addProjectDTO.DueDate,
                Status = ProjectStatus.Pending,
                ProjectMangerID = addProjectDTO.ProjectManagerID,
                TeamLeaderID = addProjectDTO.TeamLeaderID,
            };
            context.MainProjects.Add(project);
            context.SaveChanges();

            // add new developers to developers list
            foreach (var dev in addProjectDTO.Developers)
            {
                context.MainProjectDevelopers.Add(new MainProjectDeveloper()
                {
                    MainProjectID = project.MainProjectID,
                    DeveloperID = dev
                });
                context.SaveChanges();
            }
        }

        public List<MainProject> GetAllMainProjectByProjectManagerID(string id)
        {
            return context.MainProjects.Where(m => m.ProjectMangerID == id).Include(s => s.Sprints).Include(d => d.MainProjectDevelopers).ThenInclude(m => m.Developer).ToList();
        }

        public List<MainProject> GetAllMainProjects()
        {
            return context.MainProjects.ToList();
        }

        public List<MainProject> GetAllMainProjectsByTeamLeaderID(string id)
        {
            return context.MainProjects.Where(t => t.TeamLeaderID == id).Include(s => s.Sprints).Include(d => d.MainProjectDevelopers).ThenInclude(m => m.Developer).ToList();
        }

        public List<MainProject> GetAllProjectsByUserID(string id)
        {
            return context.MainProjects.Include(t => t.TeamLeader).Include(d => d.MainProjectDevelopers).ThenInclude(dev => dev.Developer).Where(m => m.ProjectMangerID == id).ToList();
        }

        public EditProjectDTO GetMainProjectByProjectID(int id)
        {
            var oldProject= context.MainProjects.Where(m => m.MainProjectID == id).SingleOrDefault();
            EditProjectDTO editProjectDTO = new EditProjectDTO()
            {
                MainProjectID = oldProject.MainProjectID,
                Title = oldProject.Title,
                Description = oldProject.Description,
                StartDate = oldProject.StartDate,
                DueDate = oldProject.DueDate
            };
            return editProjectDTO;
        }

        public void UpdateProject(EditProjectDTO editProjectDTO)
        {
            //add new info for the main project
            var oldPro = context.MainProjects.Where(p => p.MainProjectID == editProjectDTO.MainProjectID).SingleOrDefault();
            oldPro.TeamLeaderID = editProjectDTO.TeamLeaderID;
            oldPro.Title = editProjectDTO.Title;
            oldPro.Description = editProjectDTO.Description;
            oldPro.StartDate = editProjectDTO.StartDate;
            oldPro.DueDate = editProjectDTO.DueDate;
            context.SaveChanges();

            var relation =context.MainProjectDevelopers.Where(p => p.MainProjectID == editProjectDTO.MainProjectID).ToList();
            foreach (var dev in relation)
            {
                context.MainProjectDevelopers.Remove(dev);
            }


            foreach (var dev in editProjectDTO.Developers)
            {
                context.MainProjectDevelopers.Add(new MainProjectDeveloper()
                {
                    MainProjectID = oldPro.MainProjectID,
                    DeveloperID = dev
                });
                context.SaveChanges();
            }
        }
    }
}

