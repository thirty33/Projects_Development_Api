﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Resources
{
    public class ProjectResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
    }
}
