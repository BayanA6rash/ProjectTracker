using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
    public interface ISTaskRepository
    {
        public List<STask> GetSTaskBySprintID(int id);
        public EditSTaskDTO GetSTaskBySTaskID(int id);

        public void AddSTask(AddSTaskDTO addSTaskDTO);
        public List<STask> GetAllSTasks();
        public List<STask> GetAllSTasksByUserIDAndSprintID(string userID,int sprintID);
        public void UpdateStask(EditSTaskDTO editSTaskDTO);
    }
}
