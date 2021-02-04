using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
    public class STaskRepository : ISTaskRepository
    {

        private ApplicationDbContext context;
        public STaskRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void AddSTask(AddSTaskDTO addSTaskDTO)
        {
            context.STasks.Add(new STask() { 

            STaskTitle = addSTaskDTO.Title,
            STaskDescription = addSTaskDTO.Description,
            Status = STaskStatus.Pending,
            DeveloperID =addSTaskDTO.DeveloperID,
            SprintID = addSTaskDTO.SprintID
            });
            context.SaveChanges();
        }

        public List<STask> GetAllSTasks()
        {
            return context.STasks.ToList();
        }

        public List<STask> GetAllSTasksByUserIDAndSprintID(string userID,int sprintID)
        {
            return context.STasks.Where(u => u.DeveloperID == userID && u.SprintID == sprintID).ToList();
        }

        public int GetSprintIDBySTaskID(int id)
        {
            return context.STasks.Where(s => s.STaskID == id).Select(p => p.SprintID).SingleOrDefault();
        }

        public List<STask> GetSTaskBySprintID(int id)
        {
            return context.STasks.Include(d => d.Developer).Where(s => s.SprintID == id).ToList();
        }

        public EditSTaskDTO GetSTaskBySTaskID(int id)
        {
            var oldSTask = context.STasks.Where(s => s.STaskID == id).Include(p => p.Sprint).Include(d => d.Developer).SingleOrDefault();
            EditSTaskDTO editSTaskDTO = new EditSTaskDTO()
            {
                STaskID= id,
                SprintID = oldSTask.SprintID,
                Title = oldSTask.STaskTitle,
                Description = oldSTask.STaskDescription,
                Status = oldSTask.Status,
            };
            return editSTaskDTO;
        }

        public void UpdateStask(EditSTaskDTO editSTaskDTO)
        {
            STask oldSTask = context.STasks.Where(s => s.STaskID == editSTaskDTO.STaskID).SingleOrDefault();
            oldSTask.STaskTitle = editSTaskDTO.Title;
            oldSTask.STaskDescription = editSTaskDTO.Description;
            oldSTask.Status = editSTaskDTO.Status;
            oldSTask.DeveloperID = editSTaskDTO.DeveloperID;
            context.SaveChanges();
        }
    }
}
