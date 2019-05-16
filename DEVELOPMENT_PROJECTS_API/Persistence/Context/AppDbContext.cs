using DEVELOPMENT_PROJECTS_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.FirstName).IsRequired().HasMaxLength(20);
            builder.Entity<User>().Property(p => p.Password).IsRequired();
            builder.Entity<User>().Property(p => p.LastName).IsRequired().HasMaxLength(20);
            builder.Entity<User>().Property(p => p.Role).IsRequired();
            builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(10);

            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<Project>().HasKey(p => p.Id);
            builder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Project>().Property(p => p.UserId).IsRequired();
            builder.Entity<Project>().Property(p => p.Description).IsRequired().HasMaxLength(100);
            builder.Entity<Project>().Property(p => p.CreationDate).IsRequired();

            builder.Entity<Job>().ToTable("Jobs");
            builder.Entity<Job>().HasKey(p => p.Id);
            builder.Entity<Job>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Job>().Property(p => p.UserId).IsRequired();
            builder.Entity<Job>().Property(p => p.Description).IsRequired().HasMaxLength(100);
            builder.Entity<Job>().Property(p => p.EnterDate).IsRequired();
            builder.Entity<Job>().Property(p => p.EndDate).IsRequired();

            builder.Entity<Project>()
                    .HasOne(p => p.User)
                    .WithMany(b => b.Projects)
                    .HasForeignKey(p => p.UserId)
                    .HasConstraintName("Fk_UserId");

            builder.Entity<Job>()
                    .HasOne(p => p.User)
                    .WithMany(b => b.Jobs)
                    .HasForeignKey(p => p.UserId)
                    .HasConstraintName("Fk_UserId");
        }
    }
}
