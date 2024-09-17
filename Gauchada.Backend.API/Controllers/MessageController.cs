using Gauchada.Backend.Model.Response;
using Gauchada.Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gauchada.Backend.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("{tripId}")]
        public async Task<ActionResult> SendMessage(int tripId, string writer, string messageContent, string userType)
        {
            try
            {
                await _messageService.SendMessage(tripId, messageContent, writer, userType);
                return StatusCode(201, ControllerResponse.SuccessResponse(null, "Message Added"));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
