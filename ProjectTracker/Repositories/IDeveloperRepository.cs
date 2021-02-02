using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
    public interface IDeveloperRepository
    {
        public void AddNewDeveloper(AddPersonDTO addPersonDTO);
        public List<Developer> GetAllDevelopers();
        public EditPersonDTO GetDeveloperByID(string id);
        public void DeleteDeveloperByID(string id);
        public void UpdateDeveloper(EditPersonDTO editPersonDTO);

        public List<MainProjectDeveloper> GetDevelopersByProjectID(int id);
    }
}
