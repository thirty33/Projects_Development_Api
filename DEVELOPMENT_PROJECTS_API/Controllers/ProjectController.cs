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
    public class ProjectController : ControllerBase
    {
        private IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }
        //Save Project
        [HttpPost("saveproject")]
        public async Task<IActionResult> PostAsync([FromBody] ProjectResource resource)
        {
            var response = new SingleResponse<SaveObjectReponse>();
            try
            {
                var project = _mapper.Map<ProjectResource, Project>(resource);
                var result = await _projectService.SaveAsync(project);

                if (!result.Success)
                    response.Message = result.Message;
                response.Model = result;
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
