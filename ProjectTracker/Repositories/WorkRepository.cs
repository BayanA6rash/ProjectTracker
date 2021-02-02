using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Data;
using ProjectTracker.Models;
using ProjectTracker.Repositories.DTO;

namespace ProjectTracker.Repositories
{
    public class WorkRepository : IWorkRepository
    {
        private ApplicationDbContext context;
        public WorkRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        public void AddWork(AddWorkDTO addWorkDTO)
        {
            Work work = new Work() {
                WorkTitle = addWorkDTO.Title,
                WorkDescription = addWorkDTO.Description,
                Status = WorkStatus.Pending,
                DeveloperID = addWorkDTO.DeveloperID,
                STaskID = addWorkDTO.STaskID,
            };
            context.Works.Add(work);
            context.SaveChanges();

            Stream st = addWorkDTO.file.OpenReadStream(); // stream file inside it

            using (BinaryReader br = new BinaryReader(st)) // binaryreader stream inside it
            {
                var byteFile = br.ReadBytes((int)st.Length); // to read bytes from file till length of the file
                work.WorkFileName = addWorkDTO.file.FileName;
                work.ContentType = addWorkDTO.file.ContentType;
                work.WorkFile = byteFile;
            }
            context.SaveChanges();
        }

        public List<Work> GetAllWorks()
        {
            return context.Works.ToList();
        }

        public List<Work> GetAllWorksByUserIDAndSTaskID(string userID, int staskID)
        {
            return context.Works.Where(w => w.DeveloperID == userID && w.STaskID == staskID).Include(d =>d.Developer).ToList();
        }

        List<Work> IWorkRepository.GetWorkBySTaskID(int id)
        {
            return context.Works.Where(w => w.STaskID == id).Include(d => d.Developer).ToList();
        }


        public EditWorkDTO GetWorkByWorkID(int id)
        {
            var oldWork = context.Works.Where(w => w.WorkID == id).SingleOrDefault();
            EditWorkDTO editWorkDTO = new EditWorkDTO()
            {
                STaskID=oldWork.STaskID,
                WorkID = id,
                Status = oldWork.Status
            };
            return editWorkDTO;
        }

        public void UpdateWork(EditWorkDTO editWorkDTO)
        {
            var oldWork = context.Works.Where(w => w.WorkID == editWorkDTO.WorkID).SingleOrDefault();
            oldWork.Status = editWorkDTO.Status;
            oldWork.RejectionNote = editWorkDTO.note;
            context.SaveChanges();

            if (editWorkDTO.Status == WorkStatus.Rejected)
            {
                SendMail(oldWork.Developer.Email, oldWork.Developer.FirstName, "dont forget", oldWork );
            }
        }

        public Work GetWork(int id)
        {
            return context.Works.Where(w => w.WorkID == id).SingleOrDefault();
        }

        private void SendMail(string email, string name, string password, Work work)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress("bayan.ala6rsh@gmail.com");
                message.Subject = "Project Tracker System";
                message.Body = $"Dear {name},<br/><br/>Your work has been rejected <br/> Work details:<br/>WorkID = {work.WorkID} <br/>WorkTitle = {work.WorkTitle}" +
                    $"<br/>WorkDescription = {work.WorkDescription} <br/>Due to these resons : {work.RejectionNote}";
                message.IsBodyHtml = true;
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(email, password);
                    smtpClient.Send(message);
                }
            }
        }
    }
}
