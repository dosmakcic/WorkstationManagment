using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkstationManagment.Models;

namespace WorkstationManagment.Data
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<WorkPosition> WorkPositions { get; set; }
        public DbSet<UserWorkPosition> UserWorkPositions { get; set; }


         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            string connectionString = "server=localhost;port=3306;database=workstation_db;user=root;password=my-secret-pw;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserWorkPosition>()
                .HasOne(uwp => uwp.User)
                .WithMany(u => u.WorkPositions)
                .HasForeignKey(uwp => uwp.UserId);

            modelBuilder.Entity<UserWorkPosition>()
                .HasOne(uwp => uwp.WorkPosition)
                .WithMany(wp => wp.UserWorkPositions)
                .HasForeignKey(uwp => uwp.WorkPositionId);

            // Seed podaci za Role tabelu
            modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", Description = "Administrator" },
            new Role { Id = 2, Name = "User", Description = "Regular user" }
        );
    }

    }
}
