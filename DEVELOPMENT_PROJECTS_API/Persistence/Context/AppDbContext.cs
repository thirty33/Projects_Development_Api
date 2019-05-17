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
            try
            {
                base.OnModelCreating(builder);

                builder.Entity<User>().ToTable("Users");
                builder.Entity<User>().HasKey(p => p.Id);
                builder.Entity<User>().Property(p => p.FirstName).IsRequired().HasMaxLength(20);
                builder.Entity<User>().Property(p => p.Password).IsRequired();
                builder.Entity<User>().Property(p => p.LastName).IsRequired().HasMaxLength(20);
                builder.Entity<User>().Property(p => p.Role).IsRequired();
                builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(10);

                builder.Entity<User>().HasData
                (
                    new User { Id = 01, FirstName = "Joel", LastName = "Suarez", Username = "joel01", Password = "admin", Role = Role.Admin },
                    new User { Id = 02, FirstName = "Gustavo", LastName = "Suarez", Username = "gustav01", Password = "user", Role = Role.User }
                );

                builder.Entity<Project>().ToTable("Projects");
                builder.Entity<Project>().HasKey(p => p.Id);
                builder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.Entity<Project>().Property(p => p.UserId).IsRequired();
                builder.Entity<Project>().Property(p => p.Description).IsRequired().HasMaxLength(100);
                builder.Entity<Project>().Property(p => p.CreationDate).IsRequired();

                builder.Entity<Project>().HasData
                (
                    new Project { Id = 01, Name = "Bingo75App", Description = "Builded with Xamarin and .net Framework", CreationDate = new DateTime(2019, 5, 14), UserId = 01 },
                    new Project { Id = 02, Name = "SIIS", Description = "builded with php and jquery", CreationDate = new DateTime(2019, 5, 14), UserId = 01 },
                    new Project { Id = 03, Name = "Jet set travel system", Description = "builded with vue.js and firebase", CreationDate = new DateTime(2019, 5, 14), UserId = 01 }
                );

                builder.Entity<Job>().ToTable("Jobs");
                builder.Entity<Job>().HasKey(p => p.Id);
                builder.Entity<Job>().Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.Entity<Job>().Property(p => p.UserId).IsRequired();
                builder.Entity<Job>().Property(p => p.Description).IsRequired().HasMaxLength(100);
                builder.Entity<Job>().Property(p => p.EnterDate).IsRequired();
                builder.Entity<Job>().Property(p => p.EndDate).IsRequired();

                builder.Entity<Job>().HasData
                (
                    new Job { Id = 01, Name = "Job1", Description = "Description One", EnterDate = new DateTime(2019, 03, 21), EndDate = new DateTime(2019, 4, 5), UserId = 01 },
                    new Job { Id = 02, Name = "Job2", Description = "Description Two", EnterDate = new DateTime(2019, 03, 21), EndDate = new DateTime(2019, 4, 5), UserId = 01 }
                );

                builder.Entity<Project>()
                        .HasOne(p => p.User)
                        .WithMany(b => b.Projects)
                        .HasForeignKey(p => p.UserId)
                        .HasConstraintName("Fk_UserId_One");

                builder.Entity<Job>()
                        .HasOne(p => p.User)
                        .WithMany(b => b.Jobs)
                        .HasForeignKey(p => p.UserId)
                        .HasConstraintName("Fk_UserId_One");
            }
            catch (Exception ex)
            {
                throw new Exception("An exeption", ex);
            }
        }
    }
}
