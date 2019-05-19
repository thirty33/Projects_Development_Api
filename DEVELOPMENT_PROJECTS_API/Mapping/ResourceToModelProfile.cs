using AutoMapper;
using DEVELOPMENT_PROJECTS_API.Models;
using DEVELOPMENT_PROJECTS_API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<ProjectResource, Project>();
        }
    }
}
