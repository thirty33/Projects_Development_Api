using DEVELOPMENT_PROJECTS_API.Domain.Repositories;
using DEVELOPMENT_PROJECTS_API.Domain.Services;
using DEVELOPMENT_PROJECTS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository,
                    IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task SendMessage(ContactMe _message)
        {
            try
            {
                await _messageRepository.SaveMessage(_message);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
