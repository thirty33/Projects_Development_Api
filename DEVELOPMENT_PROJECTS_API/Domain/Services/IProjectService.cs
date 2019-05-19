using DEVELOPMENT_PROJECTS_API.Helpers;
using DEVELOPMENT_PROJECTS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Domain.Services
{
    public interface IProjectService
    {
        Task<SaveObjectReponse> SaveAsync(Project project);
    }
}
