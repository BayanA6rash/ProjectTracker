using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
   public interface ISprintRepository
    {
        public List<Sprint> GetSprintByProjectID(int id);
        public EditSprintDTO GetSprintBySprintID(int id);

        public void AddSprint(AddSprintDTO addSprintDTO);
        public List<Sprint> GetAllSprintsByTeamLeaderIDandProjectID(string userID, int proID);

        public void UpdateSprint(EditSprintDTO editSprintDTO);

        public int GetProjectIDBySprintID(int id);
    }
}
