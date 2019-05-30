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
        public DbSet<ContactMe> Requirements { get; set; }

        public DbSet<UserInformation> UserInformations  { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            try
            {
                base.OnModelCreating(builder);
                builder.ForNpgsqlUseIdentityColumns();

                builder.Entity<User>().ToTable("Users");
                builder.Entity<User>().HasKey(p => p.Id);
                builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Entity<User>().Property(p => p.FirstName).IsRequired();
                builder.Entity<User>().Property(p => p.Password).IsRequired();
                builder.Entity<User>().Property(p => p.LastName).IsRequired();
                builder.Entity<User>().Property(p => p.Role).IsRequired();
                builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(20);
                builder.Entity<User>().HasMany(p => p.Projects).WithOne(e => e.User);
                builder.Entity<User>().HasMany(p => p.Jobs).WithOne(e => e.User);
                builder.Entity<User>().HasMany(p => p.UserInfo).WithOne(e => e.User);

                builder.Entity<User>().HasData
                (
                    new User { Id = 01, FirstName = "Joel", LastName = "Suarez", Username = "admin", Password = "admin", Role = Role.Admin }
                );

                builder.Entity<Project>().ToTable("Projects");
                builder.Entity<Project>().HasKey(p => p.Id);
                builder.Entity<Project>().Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.Entity<Project>().Property(p => p.UserId).IsRequired();
                builder.Entity<Project>().Property(p => p.Description).IsRequired().HasMaxLength(100);
                builder.Entity<Project>().Property(p => p.CreationDate).IsRequired();

                builder.Entity<Project>().HasData
                (
                    new Project { Id = 01, Name = "project_name_one", Description = "project_description_one", CreationDate = new DateTime(2019, 5, 14), UserId = 01 },
                    new Project { Id = 02, Name = "project_name_two", Description = "project_description_two", CreationDate = new DateTime(2019, 5, 14), UserId = 01 },
                    new Project { Id = 03, Name = "project_name_three", Description = "project_description_three", CreationDate = new DateTime(2019, 5, 14), UserId = 01 }
                );

                builder.Entity<Job>().ToTable("Jobs");
                builder.Entity<Job>().HasKey(p => p.Id);
                builder.Entity<Job>().Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Entity<Job>().Property(p => p.Name).IsRequired().HasMaxLength(50);
                builder.Entity<Job>().Property(p => p.UserId).IsRequired();
                builder.Entity<Job>().Property(p => p.Description).IsRequired().HasMaxLength(100);
                builder.Entity<Job>().Property(p => p.EnterDate).IsRequired();
                builder.Entity<Job>().Property(p => p.EndDate).IsRequired();

                builder.Entity<Job>().HasData
                (
                    new Job { Id = 01, Name = "job_name_one", Description = "job_description_one", EnterDate = new DateTime(2019, 03, 21), EndDate = new DateTime(2019, 4, 5), UserId = 01 },
                    new Job { Id = 02, Name = "job_name_two", Description = "job_description_two", EnterDate = new DateTime(2019, 03, 21), EndDate = new DateTime(2019, 4, 5), UserId = 01 }
                );

                builder.Entity<UserInformation>().ToTable("UserInformations");
                builder.Entity<UserInformation>().HasKey(p => p.Id);
                builder.Entity<UserInformation>().Property(p => p.InformationTitle).IsRequired();
                builder.Entity<UserInformation>().Property(p => p.InformationItems).IsRequired();

                builder.Entity<UserInformation>().HasData
                (
                    new UserInformation
                    {
                        Id = 01,
                        InformationTitle = "Estudios",
                        InformationItems = new List<string>
                        {
                            "5to Semestre Ing. de Sistemas",
                            "Desarrollo web en academias virtuales (Platzi, Udemy, etc)",
                            "Redes Cisco (Cisco Academy)",
                        },
                        UserId = 01
                    },                    
                    new UserInformation
                    {
                        Id = 02,
                        InformationTitle = "Conocimientos",
                        InformationItems = new List<string>
                        {
                            "Maquetacion web (css-grid, flexbox, responsive desing, jquery)",
                            "Arquitectura MVC, MVVM, REST API, Patrones de diseno",
                            "Manejo de Servicios (Postman, Swagger)",
                            "Bases de datos (MYSQL, POSTGRESQL)",
                            "Control de Versiones (Git)",
                            "Organizacion de Tareas (Trello)",
                        },
                        UserId = 01
                    },
                    new UserInformation
                    {
                        Id = 03,
                        InformationTitle = "Lenguajes",
                        InformationItems = new List<string>
                        {
                            "Javascript",
                            "C#",
                            "PHP",
                            "C++",
                            "Python"
                        },
                        UserId = 01
                    },
                    new UserInformation
                    {
                        Id = 04,
                        InformationTitle = "Frameworks",
                        InformationItems = new List<string>
                        {
                            "Vue.js",
                            "Xamarin Forms",
                            ".Net Core",
                            ".Net Framework",
                            "Xamarin Forms",
                            "Firebase",
                        },
                        UserId = 01
                    }
                );

                builder.Entity<ContactMe>().ToTable("Requirements");
                builder.Entity<ContactMe>().HasKey(p => p.Id);
                builder.Entity<ContactMe>().Property(p => p.Id).ValueGeneratedOnAdd();
                builder.Entity<ContactMe>().Property(p => p.Name).IsRequired();
                builder.Entity<ContactMe>().Property(p => p.MessageSubject).IsRequired().HasMaxLength(100);
                builder.Entity<ContactMe>().Property(p => p.Message).IsRequired().HasMaxLength(900);


                builder.Entity<Project>()
                        .HasOne(p => p.User)
                        .WithMany(b => b.Projects)
                        .HasForeignKey(p => p.UserId)
                        .HasConstraintName("UserId");

                builder.Entity<Job>()
                        .HasOne(p => p.User)
                        .WithMany(b => b.Jobs)
                        .HasForeignKey(p => p.UserId)
                        .HasConstraintName("UserId");

                builder.Entity<UserInformation>()
                        .HasOne(p => p.User)
                        .WithMany(b => b.UserInfo)
                        .HasForeignKey(p => p.UserId)
                        .HasConstraintName("UserId");
            }
            catch (Exception ex)
            {
                throw new Exception("An exeption", ex);
            }
        }
    }
}
