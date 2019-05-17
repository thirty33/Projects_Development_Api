using DEVELOPMENT_PROJECTS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Domain.Repositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> ListJobsByUser(int userId);
        Task<Job> CreateJob(Job @object);
        Task<Job> DeleteJob(Job @object);
        Task<Job> ModifyJob(int jobId, Job @object);
    }
}
