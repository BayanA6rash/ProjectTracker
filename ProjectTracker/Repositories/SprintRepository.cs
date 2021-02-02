using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Data;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
    public class SprintRepository : ISprintRepository
    {
        private ApplicationDbContext context;
        public SprintRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void AddSprint(AddSprintDTO addSprintDTO)
        {
            Sprint sprint = new Sprint()
            {
                MainProjectID = addSprintDTO.MainProjectID,
                SprintTitle = addSprintDTO.Title,
                SprintDescription = addSprintDTO.Description,
                StartDate = addSprintDTO.StartDate,
                DueDate = addSprintDTO.DueDate,
                Status = SprintStatus.Pending,
                TeamLeaderID = addSprintDTO.TeamLeaderID,
            };
            context.Sprints.Add(sprint);
            context.SaveChanges();
        }

        public List<Sprint> GetAllSprintsByTeamLeaderIDandProjectID(string userID, int proID)
        {
            return context.Sprints.Where(t => t.TeamLeaderID == userID && t.MainProjectID == proID).ToList();
        }

        public int GetProjectIDBySprintID(int id)
        {
            return context.Sprints.Where(s => s.SprintID == id).Select(p => p.MainProjectID).SingleOrDefault();
        }

        public List<Sprint> GetSprintByProjectID(int id)
        {
            return context.Sprints.Where(t => t.MainProjectID == id).ToList();
        }

        public EditSprintDTO GetSprintBySprintID(int id)
        {
            var oldSprint = context.Sprints.Where(s => s.SprintID == id).SingleOrDefault();
            EditSprintDTO editSprintDTO = new EditSprintDTO()
            {
                SprintID = id,
                MainProjectID = oldSprint.MainProjectID,
                Title = oldSprint.SprintTitle,
                Description = oldSprint.SprintDescription,
                StartDate = oldSprint.StartDate,
                DueDate=oldSprint.DueDate,
                Status = oldSprint.Status
            };
            return editSprintDTO;
        }
    
        public void UpdateSprint(EditSprintDTO editSprintDTO)
        {
          var oldSprint= context.Sprints.Where(s => s.SprintID == editSprintDTO.SprintID).SingleOrDefault();
            oldSprint.SprintTitle = editSprintDTO.Title;
            oldSprint.SprintDescription = editSprintDTO.Description;
            oldSprint.Status = editSprintDTO.Status;
            oldSprint.StartDate = editSprintDTO.StartDate;
            oldSprint.DueDate = editSprintDTO.DueDate;

            context.SaveChanges();
        }
    }
}
    