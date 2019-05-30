using DEVELOPMENT_PROJECTS_API.Domain.Services;
using DEVELOPMENT_PROJECTS_API.Helpers;
using DEVELOPMENT_PROJECTS_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : Controller
    {
        private IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [AllowAnonymous]
        [HttpPost("sendmessage")]
        public async Task<IActionResult> PostAsync([FromBody] ContactMe resource)
        {
            var response = new SingleResponse<String>();
            try
            {
                await _messageService.SendMessage(resource);
                response.Model = "message has been sended";
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support," + ex;
            }
            return response.ToHttpResponse();
        }
    }
}
