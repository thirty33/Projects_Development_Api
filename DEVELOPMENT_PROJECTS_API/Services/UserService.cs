using DEVELOPMENT_PROJECTS_API.Domain.Repositories;
using DEVELOPMENT_PROJECTS_API.Domain.Services;
using DEVELOPMENT_PROJECTS_API.Helpers;
using DEVELOPMENT_PROJECTS_API.Models;
using DEVELOPMENT_PROJECTS_API.Resources;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository,
                IProjectRepository projectRepository, IJobRepository jobRepository, IUnitOfWork unitOfWork,
                ITokenManager tokenManager)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager; 
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _userRepository.FindByUserAndPassword(username, password);

            //User not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public async Task<IEnumerable<Project>> GetProjectsByUser(int userId)
        {
            return await _projectRepository.ListProjectByUser(userId);
        }

        public async Task<IEnumerable<Job>> GetJobsByUser(int userId)
        {
            return await _jobRepository.ListJobsByUser(userId);
        }

        public async Task<User> RegisterUser(User user)
        {
            try
            {
                //var newUser = new User() { Username = username, Password = password };
                await _userRepository.SaveUserAsync(user);
                await _unitOfWork.CompleteAsync();
                return user;
            }
            catch (Exception e)
            {
                return null; 
            }

        }

        public async Task<UserResource> GetAllUserData(int _userId)
        {
            var result = await _userRepository.ReturnUserData(_userId);
            return result;
        }
    }
}
