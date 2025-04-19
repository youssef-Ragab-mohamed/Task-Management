using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskManagement.Models;

namespace TaskManagement.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User, ApplicationRole, int>
    {

        public AppDbContext(DbContextOptions<AppDbContext> op) : base(op)
        {

        }
       
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
       
        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<TaskEntity>().Property(t => t.Status).HasConversion<string>();

            mb.Entity<TaskEntity>().Property(t => t.Priority).HasConversion<string>();
            mb.Entity<TaskHistory>().Property(th => th.NewStatus).HasConversion<string>();
            mb.Entity<TaskHistory>().Property(th => th.OldStatus).HasConversion<string>();
            mb.Entity<TaskHistory>().Property(th => th.Action).HasConversion<string>();
             



        }
    }
}
