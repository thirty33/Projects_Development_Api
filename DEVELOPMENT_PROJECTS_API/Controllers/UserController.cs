using AutoMapper;
using DEVELOPMENT_PROJECTS_API.Domain.Services;
using DEVELOPMENT_PROJECTS_API.Helpers;
using DEVELOPMENT_PROJECTS_API.Models;
using DEVELOPMENT_PROJECTS_API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Authenticate([FromBody]User userParam)
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
    }
}
