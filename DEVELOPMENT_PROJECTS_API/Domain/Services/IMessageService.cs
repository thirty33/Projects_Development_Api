﻿using DEVELOPMENT_PROJECTS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Domain.Services
{
    public interface IMessageService
    {
        Task SendMessage(ContactMe _message);
    }
}
