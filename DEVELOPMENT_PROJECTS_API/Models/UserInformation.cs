using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Models
{
    public class UserInformation
    {
        public int Id { get; set; }
        public string InformationTitle { get; set; }
        public List<string> InformationItems { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
