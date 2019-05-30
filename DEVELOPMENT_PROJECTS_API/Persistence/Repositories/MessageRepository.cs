using DEVELOPMENT_PROJECTS_API.Domain.Repositories;
using DEVELOPMENT_PROJECTS_API.Models;
using DEVELOPMENT_PROJECTS_API.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Persistence.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {

        }

        public async Task SaveMessage(ContactMe _message)
        {
            await _context.Requirements.AddAsync(_message);
        }
    }
}
