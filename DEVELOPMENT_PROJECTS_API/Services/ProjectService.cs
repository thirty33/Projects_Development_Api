﻿using DEVELOPMENT_PROJECTS_API.Domain.Repositories;
using DEVELOPMENT_PROJECTS_API.Domain.Services;
using DEVELOPMENT_PROJECTS_API.Helpers;
using DEVELOPMENT_PROJECTS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IProjectRepository projectRepository,
                    IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Project> SaveAsync(Project project)
        {
            try
            {
                await _projectRepository.AddAsync(project);
                await _unitOfWork.CompleteAsync();

                return project;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Project> UpdateAsync(int id, Project project)
        {
            var existingProject = await _projectRepository.FindByProjectIdAsync(id);
            if (existingProject == null)
                return null;

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            existingProject.CreationDate = project.CreationDate;
            existingProject.Photo = project.Photo;

            try
            {
                _projectRepository.Update(existingProject);
                await _unitOfWork.CompleteAsync();

                return existingProject;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Project> DeleteAsync(int id)
        {
            var existingProject = await _projectRepository.FindByProjectIdAsync(id);
            if (existingProject == null)
                return null;
            try
            {
                _projectRepository.Remove(existingProject);
                await _unitOfWork.CompleteAsync();

                return existingProject;
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return null;
            }
        }

    }
}
