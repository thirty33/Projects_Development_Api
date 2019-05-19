using DEVELOPMENT_PROJECTS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> ListProjectByUser(int userId);
        Task AddAsync(Project project);
        //Task<Project> DeleteProject(Project @object);
        //Task<Project> ModifyProject(int ProjectId, Project @object);
    }
}
