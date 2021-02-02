using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Data;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace ProjectTracker.Repositories
{
    public class ProjectManagerRepository : IProjectManagerRepository
    {
        private ApplicationDbContext context;
        public ProjectManagerRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public static PasswordHasher passwordHasher = new PasswordHasher();

        public void AddNewProjectManager(AddPersonDTO addPersonDTO)
        {
            ProjectManager projectManager = new ProjectManager()
            {
                UserName = addPersonDTO.Email,
                Email = addPersonDTO.Email,
                FirstName = addPersonDTO.FirstName,
                LastName = addPersonDTO.LastName,
                Age = addPersonDTO.Age,
                EmailConfirmed = true,
                PasswordHash = passwordHasher.HashPassword(addPersonDTO.Password),
                NormalizedEmail = addPersonDTO.Email.ToUpper(),
                NormalizedUserName = addPersonDTO.Email.ToUpper()
            };
            context.ProjectManagers.Add(projectManager);
            context.SaveChanges();

            context.UserRoles.Add(new IdentityUserRole<string>()
            {
                UserId = projectManager.Id,
                RoleId = "2"
            });
            context.SaveChanges();
        }

        public void DeleteProjectManagerByID(string id)
        {
            context.ProjectManagers.Attach(context.ProjectManagers.Where(d => d.Id == id).FirstOrDefault());
            context.ProjectManagers.Remove(context.ProjectManagers.Where(d => d.Id == id).FirstOrDefault());
            context.SaveChanges();
        }

        public List<ProjectManager> GetAllProjectManagers()
        {
            return context.ProjectManagers.ToList();
        }

        public EditPersonDTO GetProjectManagerByID(string id)
        {
            var manager = context.ProjectManagers.Where(d => d.Id == id).SingleOrDefault();

            return new EditPersonDTO()
            {
                PersonID = id,
                FirstName = manager.FirstName,
                LastName = manager.LastName,
                Age = manager.Age,
                Email = manager.Email
            };
        }

        public void UpdateProjectManager(EditPersonDTO editPersonDTO)
        {
            var oldManager = context.ProjectManagers.Where(d => d.Id == editPersonDTO.PersonID).SingleOrDefault();

            oldManager.Id = editPersonDTO.PersonID;
            oldManager.UserName = editPersonDTO.Email;
            oldManager.Email = editPersonDTO.Email;
            oldManager.FirstName = editPersonDTO.FirstName;
            oldManager.LastName = editPersonDTO.LastName;
            oldManager.Age = editPersonDTO.Age;
            oldManager.PasswordHash = passwordHasher.HashPassword(editPersonDTO.Password);

            context.SaveChanges();
        }
    }
}
