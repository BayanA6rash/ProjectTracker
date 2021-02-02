using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Data;
using ProjectTracker.Repositories.DTO;
using Microsoft.AspNet.Identity;
using ProjectTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace ProjectTracker.Repositories
{
    public class TeamLeaderRepository: ITeamLeaderRepository
    {
        private ApplicationDbContext context;
        public TeamLeaderRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public static PasswordHasher passwordHasher = new PasswordHasher();

        public void AddNewTeamLeader(AddPersonDTO addPersonDTO)
        {
TeamLeader teamLeader = new TeamLeader(){
                NormalizedEmail = addPersonDTO.Email.ToUpper(),
                NormalizedUserName = addPersonDTO.Email.ToUpper(),
                UserName = addPersonDTO.Email,
                Email = addPersonDTO.Email,
                FirstName = addPersonDTO.FirstName,
                LastName = addPersonDTO.LastName,
                Age = addPersonDTO.Age,
                EmailConfirmed = true,
                PasswordHash = passwordHasher.HashPassword(addPersonDTO.Password),
            };
            context.TeamLeaders.Add(teamLeader);
            context.SaveChanges();

            context.UserRoles.Add(new IdentityUserRole<string>()
            {
                UserId = teamLeader.Id,
                RoleId = "3"
            });
            context.SaveChanges();
        }

        public List<Models.TeamLeader> GetAllTeamLeaders()
        {
            return context.TeamLeaders.ToList();
        }

        public EditPersonDTO GetTeamLeaderByID(string id)
        {
            var leader = context.TeamLeaders.Where(d => d.Id == id).SingleOrDefault();

            return new EditPersonDTO()
            {
                PersonID = id,
                FirstName = leader.FirstName,
                LastName = leader.LastName,
                Age = leader.Age,
                Email = leader.Email
            };
        }

        public void DeleteTeamLeaderByID(string id)
        {
            context.TeamLeaders.Attach(context.TeamLeaders.Where(d => d.Id == id).FirstOrDefault());
            context.TeamLeaders.Remove(context.TeamLeaders.Where(d => d.Id == id).FirstOrDefault());
            context.SaveChanges();
        }

        public void UpdateTeamLeader(EditPersonDTO editPersonDTO)
        {
            var oldLeader = context.TeamLeaders.Where(d => d.Id == editPersonDTO.PersonID).SingleOrDefault();

            oldLeader.Id = editPersonDTO.PersonID;
            oldLeader.UserName = editPersonDTO.Email;
            oldLeader.Email = editPersonDTO.Email;
            oldLeader.FirstName = editPersonDTO.FirstName;
            oldLeader.LastName = editPersonDTO.LastName;
            oldLeader.Age = editPersonDTO.Age;
            oldLeader.PasswordHash = passwordHasher.HashPassword(editPersonDTO.Password);

            context.SaveChanges();
        }
    }
}
