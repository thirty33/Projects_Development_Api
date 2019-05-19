using DEVELOPMENT_PROJECTS_API.Domain.Repositories;
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

        public async Task<SaveObjectReponse> SaveAsync(Project project)
        {
            try
            {
                await _projectRepository.AddAsync(project);
                await _unitOfWork.CompleteAsync();

                return new SaveObjectReponse(project);
            }
            catch (Exception ex)
            {
                return new SaveObjectReponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<SaveObjectReponse> UpdateAsync(int id, Project project)
        {
            var existingProject = await _projectRepository.FindByProjectIdAsync(id);
            if (existingProject == null)
                return new SaveObjectReponse("Category not found.");

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            existingProject.CreationDate = project.CreationDate;
            try
            {
                _projectRepository.Update(existingProject);
                await _unitOfWork.CompleteAsync();

                return new SaveObjectReponse(existingProject);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SaveObjectReponse($"An error occurred when updating the category: {ex.Message}");
            }
        }

    }
}
