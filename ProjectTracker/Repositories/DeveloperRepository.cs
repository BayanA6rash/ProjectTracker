using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Data;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProjectTracker.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private ApplicationDbContext context;
        public DeveloperRepository(ApplicationDbContext _context)
        {
            context = _context;
        } 
        public void AddNewDeveloper(AddPersonDTO addPersonDTO)
        {
            Developer developer = new Developer()
            {
                UserName = addPersonDTO.Email,
                NormalizedEmail = addPersonDTO.Email.ToUpper(),
                NormalizedUserName = addPersonDTO.Email.ToUpper(),
                Email = addPersonDTO.Email,
                FirstName = addPersonDTO.FirstName,
                LastName = addPersonDTO.LastName,
                Age = addPersonDTO.Age,
                EmailConfirmed = true,
                PasswordHash = passwordHasher.HashPassword(addPersonDTO.Password),
            };
            context.Developers.Add(developer);
            context.SaveChanges();

            context.UserRoles.Add(new IdentityUserRole<string>()
            {
                UserId = developer.Id,
                RoleId = "4"
            });

            context.SaveChanges();

        }

        public void DeleteDeveloperByID(string id)
        {
            context.Developers.Attach(context.Developers.Where(d => d.Id == id).FirstOrDefault());
            context.Developers.Remove(context.Developers.Where(d => d.Id == id).FirstOrDefault());
            context.SaveChanges();
        }

        public List<Developer> GetAllDevelopers()
        {
            return context.Developers.ToList();
        }

        public EditPersonDTO GetDeveloperByID(string id)
        {
            var dev= context.Developers.Where(d => d.Id == id).SingleOrDefault();
           
            return new EditPersonDTO() { 
                PersonID= id,
            FirstName = dev.FirstName,
            LastName =dev.LastName,
            Age = dev.Age,
            Email= dev.Email
            };
        }

        public static PasswordHasher passwordHasher = new PasswordHasher();
        public void UpdateDeveloper(EditPersonDTO editPersonDTO)
        {
            var oldDev = context.Developers.Where(d => d.Id == editPersonDTO.PersonID).SingleOrDefault();
                oldDev.Id= editPersonDTO.PersonID;
            oldDev.UserName = editPersonDTO.Email;
            oldDev.Email = editPersonDTO.Email;
            oldDev.FirstName = editPersonDTO.FirstName;
            oldDev.LastName = editPersonDTO.LastName;
            oldDev.Age = editPersonDTO.Age;
            oldDev.PasswordHash = passwordHasher.HashPassword(editPersonDTO.Password);

            context.SaveChanges();
        }

        public List<MainProjectDeveloper> GetDevelopersByProjectID(int id)
        {
            return context.MainProjectDevelopers.Where(p => p.MainProjectID == id).Include(d => d.Developer).ToList();
        }

      
    }
}
