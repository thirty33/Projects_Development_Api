using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Models
{
    public class ContactMe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MessageSubject { get; set; }
        public string Message { get; set; }
    }
}
