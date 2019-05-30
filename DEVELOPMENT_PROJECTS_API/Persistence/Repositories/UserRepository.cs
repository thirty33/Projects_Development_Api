using AutoMapper;
using DEVELOPMENT_PROJECTS_API.Domain.Repositories;
using DEVELOPMENT_PROJECTS_API.Models;
using DEVELOPMENT_PROJECTS_API.Persistence.Context;
using DEVELOPMENT_PROJECTS_API.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IMapper _mapper;
        public UserRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        //Find a User Item into DB
        public async Task<User> FindByUserAndPassword(string _userName, string _password)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.Username == _userName && i.Password == _password);
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task SaveUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<UserResource> ReturnUserData(int _userId)
        {
            //return await (_context.Users
            //    .Include(a => a.Projects)
            //    .Include(a => a.Jobs)
            //    .Include(a => a.UserInfo)
            //    .ToListAsync());
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == _userId);
            var userResource = _mapper.Map<User, UserResource>(user);
            var projects = await _context.Projects.Where(p => p.UserId == user.Id).ToListAsync();
            var projectsResource = _mapper.Map<List<Project>, List<ProjectResource>>(projects);
            userResource.Projects = projectsResource;
            var jobs = await _context.Jobs.Where(p => p.UserId == user.Id).ToListAsync();
            var jobsResource = _mapper.Map<List<Job>, List<JobResource>>(jobs);
            userResource.Jobs = jobsResource;
            var userInfo = await _context.UserInformations.Where(p => p.UserId == user.Id).ToListAsync();
            var userInfoResource = _mapper.Map<List<UserInformation>, List<UserInformationResource>>(userInfo);
            userResource.UserInfo = userInfoResource;
            return userResource;
        }
    }
}
    