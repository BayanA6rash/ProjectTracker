using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectTracker.Models;

namespace ProjectTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<ProjectManager> ProjectManagers { set; get; }
        public virtual DbSet<TeamLeader> TeamLeaders { set; get; }
        public virtual DbSet<Developer> Developers { set; get; }
        public virtual DbSet<MainProject> MainProjects { set; get; }
        public virtual DbSet<Sprint> Sprints { set; get; }
        public virtual DbSet<STask> STasks { set; get; }
        public virtual DbSet<Work> Works { set; get; }
        public virtual DbSet<MainProjectDeveloper> MainProjectDevelopers { set; get; }
        public virtual DbSet<UserInfo> UserInfos { set; get; }
        public virtual DbSet<Admin> Admins { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MainProjectDeveloper>()
        .HasKey(c => new { c.MainProjectID, c.DeveloperID });
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
