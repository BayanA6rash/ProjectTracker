using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
    public interface IProjectManagerRepository
    {
        public void AddNewProjectManager(AddPersonDTO addPersonDTO);
        public List<ProjectManager> GetAllProjectManagers();
        public EditPersonDTO GetProjectManagerByID(string id);
        public void DeleteProjectManagerByID(string id);
        public void UpdateProjectManager(EditPersonDTO editPersonDTO);
    }
}
