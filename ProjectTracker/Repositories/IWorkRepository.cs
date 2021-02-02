using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
   public interface IWorkRepository
    {
        public List<Work> GetWorkBySTaskID(int id);
        public EditWorkDTO GetWorkByWorkID(int id);
        public void AddWork(AddWorkDTO addWorkDTO);
        public List<Work> GetAllWorks();
        public List<Work> GetAllWorksByUserIDAndSTaskID(string userID, int staskID);
        public void UpdateWork(EditWorkDTO editWorkDTO);

        public Work GetWork(int id);
    }
}
