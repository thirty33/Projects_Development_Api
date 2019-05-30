using DEVELOPMENT_PROJECTS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Domain.Repositories
{
    public interface IUserRepository
    {
        //Task<IEnumerable<User>> ListUsersAsync();
        Task<User> FindByUserAndPassword(string _userName, string _password);
        Task<User> GetById(int id);
        //Task<User> DeleteUserById(int id);
        //Task<User> RegisterUser(User @object);

        Task SaveUserAsync(User user);
    }
}
