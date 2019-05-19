using DEVELOPMENT_PROJECTS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Helpers
{
    public class SaveObjectReponse : BaseResponse
    {
        public Object Object { get; private set; }

        private SaveObjectReponse(bool success, string message, Object @object) : base(success, message)
        {
            Object = @object;
        }

        public SaveObjectReponse(Object @object) : this(true, string.Empty, @object)
        { }

        public SaveObjectReponse(string message) : this(false, message, null)
        { }
    }
}
