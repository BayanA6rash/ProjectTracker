using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
    public interface ITeamLeaderRepository
    {
        public void AddNewTeamLeader(AddPersonDTO addPersonDTO);
        public List<TeamLeader> GetAllTeamLeaders();
        public EditPersonDTO GetTeamLeaderByID(string id);
        public void DeleteTeamLeaderByID(string id);
        public void UpdateTeamLeader(EditPersonDTO editPersonDTO);
    }
}
