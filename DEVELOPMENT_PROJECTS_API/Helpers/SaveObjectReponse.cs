using DEVELOPMENT_PROJECTS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Helpers
{
    public class SaveObjectReponse : BaseResponse
    {
        public Project Project { get; private set; }

        private SaveObjectReponse(bool success, string message, Project project) : base(success, message)
        {
            Project = project;
        }

        public SaveObjectReponse(Project project) : this(true, string.Empty, project)
        { }

        public SaveObjectReponse(string message) : this(false, message, null)
        { }
    }
}
