using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
    public interface IMainProjectRepository
    {
        public EditProjectDTO GetMainProjectByProjectID(int id);
        public void AddMainProject(AddProjectDTO addProjectDTO);
        public List<MainProject> GetAllMainProjects();
        public List<MainProjectDeveloper> GetAllProjectsByUserID(string id);
        public void UpdateProject(EditProjectDTO editProjectDTO);
        public List<MainProject> GetAllMainProjectsByTeamLeaderID(string id);

        public List<MainProject> GetAllMainProjectByProjectManagerID(string id);
    }
}
