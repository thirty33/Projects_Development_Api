using AutoMapper;
using DEVELOPMENT_PROJECTS_API.Domain.Services;
using DEVELOPMENT_PROJECTS_API.Helpers;
using DEVELOPMENT_PROJECTS_API.Models;
using DEVELOPMENT_PROJECTS_API.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;
        public UserController(IUserService userService, IMapper mapper, ITokenManager tokenManager)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Authenticate([FromBody] User userParam)
        {
            var response = new SingleResponse<UserResource>();
            try
            {
                var user = await _userService.Authenticate(userParam.Username, userParam.Password);
                if (user == null)
                    response.Message = "Username or password is incorrect";
                else
                {
                    var resource = _mapper.Map<User, UserResource>(user);
                    response.Model = resource;
                }
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support," + ex;
            }
            return response.ToHttpResponse();
        }


        [AllowAnonymous]
        [HttpGet("getprojectsbyid/{id}")]
        public async Task<IActionResult> getProjectsByUserId(int id)
        {
            var response = new ListResponse<Project>();
            try
            {
                var listProjects = await _userService.GetProjectsByUser(id);
                if (listProjects.Count() == 0 || listProjects == null)
                    response.Message = "No items in stock";
                else
                {
                    var resource = _mapper.Map<IEnumerable<Project>>(listProjects);
                    response.Model = resource;
                }
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support," + ex;
            }
            return response.ToHttpResponse();
        }

        [AllowAnonymous]
        [HttpGet("getalluserdata/{id}")]
        public async Task<IActionResult> getUserData(int id)
        {
            var response = new SingleResponse<UserResource>();
            try
            {
                var user = await _userService.GetAllUserData(id);
                response.Model = user;
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support," + ex;
            }
            return response.ToHttpResponse();
        }

        [AllowAnonymous]
        [HttpGet("getjobsbyid/{id}")]
        public async Task<IActionResult> getJobsByUserId(int id)
        {
            var response = new ListResponse<Job>();
            try
            {
                var listJobs = await _userService.GetJobsByUser(id);
                if (listJobs.Count() == 0 || listJobs == null)
                    response.Message = "No items in stock";
                else
                {
                    var resource = _mapper.Map<IEnumerable<Job>>(listJobs);
                    response.Model = resource;
                }
            }
            catch(Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support," + ex;
            }
            return response.ToHttpResponse();
        }

        //Register User
        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            var response = new SingleResponse<User>();
            try
            {
                var resource = await _userService.RegisterUser(user);
                if (resource == null)
                    response.Message = "user not registered";
                response.Model = _mapper.Map<User>(resource);
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support," + ex;
            }
            return response.ToHttpResponse();
        }

        //SignOut
        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOut()
        {
            var response = new SingleResponse<string>();
            await _tokenManager.DeactivateCurrentAsync();
            response.Model = "user session closed";
            return response.ToHttpResponse();
        }
    }
}
