using DEVELOPMENT_PROJECTS_API.Models;
using DEVELOPMENT_PROJECTS_API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Domain.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<Project>> GetProjectsByUser(int userId);
        Task<IEnumerable<Job>> GetJobsByUser(int userId);

        Task<User> RegisterUser(User user);

        Task<UserResource> GetAllUserData(int _userId);
        //Task<string> SendNotification(Object @form);
        //Task<User> RegisterUser(string username, string password);
        //Task<User> DeleteUser(User @user);
        //Task<User> CreateUser(User @user);
    }
}
