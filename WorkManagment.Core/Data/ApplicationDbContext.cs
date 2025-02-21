using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Core.Models;
using BCrypt.Net;

namespace WorkstationManagment.Core.Data
{
   public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<WorkPosition> WorkPositions { get; set; }
        public DbSet<UserWorkPosition> UserWorkPositions { get; set; }

      


        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                        .HasOne(u => u.Role)      
                        .WithMany(r => r.Users)   
                        .HasForeignKey(u => u.RoleId) 
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserWorkPosition>()
                        .HasOne(uwp => uwp.User)
                        .WithMany(u => u.UserWorkPositions)
                        .HasForeignKey(uwp => uwp.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserWorkPosition>()
                        .HasOne(uwp => uwp.WorkPosition)
                        .WithMany(wp => wp.UserWorkPositions)
                        .HasForeignKey(uwp => uwp.WorkPositionId)
                        .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", Description = "Administrator" },
            new Role { Id = 2, Name = "User", Description = "Regular user" }
            );

            modelBuilder.Entity<WorkPosition>().HasData(
            new WorkPosition { Id = 1, Name = "Software Engineer" },
            new WorkPosition { Id = 2, Name = "System Administrator" }
            );

            string adminPassword = BCrypt.Net.BCrypt.HashPassword("johndoe123");

            modelBuilder.Entity<User>().HasData(
            new User { Id=1, FirstName="John", LastName="Doe", Username="john123", Password=adminPassword, RoleId=1 }
            );

            modelBuilder.Entity<UserWorkPosition>().HasData(
            new UserWorkPosition { Id=-1, UserId = 1, WorkPositionId = 1 }
            );
        }

    }
}
