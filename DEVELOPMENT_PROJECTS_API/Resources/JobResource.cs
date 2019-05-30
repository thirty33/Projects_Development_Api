using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Resources
{
    public class JobResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
    }
}
