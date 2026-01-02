using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Coordinator> Coordinators { get; set; }
        public DbSet<DefenseSchedule> DefenseSchedules { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        
            // User ↔ Notifications
            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student ↔ User
            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student ↔ Team
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(s => s.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Supervisor ↔ User
            modelBuilder.Entity<Supervisor>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Coordinator ↔ User
            modelBuilder.Entity<Coordinator>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Team ↔ Leader
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Leader)
                .WithMany()
                .HasForeignKey(t => t.LeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Team ↔ Supervisor
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Supervisor)
                .WithMany(s => s.SupervisedTeams)
                .HasForeignKey(t => t.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProjectTask ↔ Team
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.Team)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProjectTask ↔ Student
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.Student)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProjectTask ↔ Supervisor
            modelBuilder.Entity<ProjectTask>()
                .HasOne(t => t.Supervisor)
                .WithMany()
                .HasForeignKey(t => t.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Meeting ↔ Team
            modelBuilder.Entity<Meeting>()
                .HasOne(m => m.Team)
                .WithMany(t => t.Meetings)
                .HasForeignKey(m => m.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Meeting ↔ Supervisor
            modelBuilder.Entity<Meeting>()
                .HasOne(m => m.Supervisor)
                .WithMany()
                .HasForeignKey(m => m.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            // DefenseSchedule ↔ Team (one-to-one)
            modelBuilder.Entity<DefenseSchedule>()
                .HasOne(d => d.Team)
                .WithOne(t => t.DefenseSchedule)
                .HasForeignKey<DefenseSchedule>(d => d.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // DefenseSchedule ↔ Coordinator (many-to-one)
            modelBuilder.Entity<DefenseSchedule>()
                .HasOne(d => d.Coordinator)
                .WithMany(c => c.ManagedDefenses)
                .HasForeignKey(d => d.CoordinatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // DefenseSchedule ↔ Supervisors (many-to-many) via join entity
            modelBuilder.Entity<DefenseScheduleSupervisor>()
                .HasKey(dss => new { dss.DefenseScheduleId, dss.SupervisorId });

            modelBuilder.Entity<DefenseScheduleSupervisor>()
                .HasOne(dss => dss.DefenseSchedule)
                .WithMany(ds => ds.DefenseSchedulesAsEvaluator)
                .HasForeignKey(dss => dss.DefenseScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DefenseScheduleSupervisor>()
                .HasOne(dss => dss.Supervisor)
                .WithMany(s => s.DefenseSchedulesAsEvaluator)
                .HasForeignKey(dss => dss.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Student>().HasIndex(s => s.UserId).IsUnique();
            modelBuilder.Entity<Supervisor>().HasIndex(s => s.UserId).IsUnique();
            modelBuilder.Entity<Coordinator>().HasIndex(c => c.UserId).IsUnique();
            modelBuilder.Entity<Team>().HasIndex(t => t.TeamName).IsUnique();

            // Default values
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Team>().Property(t => t.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Team>().Property(t => t.ProjectPhase).HasDefaultValue("Proposal");
            modelBuilder.Entity<ProjectTask>().Property(t => t.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<ProjectTask>().Property(t => t.Status).HasDefaultValue("Pending");
            modelBuilder.Entity<Meeting>().Property(m => m.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Meeting>().Property(m => m.Status).HasDefaultValue("Requested");
            modelBuilder.Entity<Notification>().Property(n => n.SentAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Notification>().Property(n => n.IsRead).HasDefaultValue(false);
            modelBuilder.Entity<DefenseSchedule>().Property(d => d.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<DefenseSchedule>().Property(d => d.Status).HasDefaultValue("Scheduled");
        }


    }
}
