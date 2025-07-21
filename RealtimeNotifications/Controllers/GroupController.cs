using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealtimeNotifications.Handlers.Group;
using System.Threading.Tasks;

namespace RealtimeNotifications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var command = new GroupCommand(userId);
            return Ok(await _mediator.Send(command));
        }
         [HttpPost("{groupName}")]
        public async Task<IActionResult> Post(string groupName)
        {
            var command = new GroupCMD(groupName);
            await _mediator.Send(command);
            return Ok("Group Created");
        } 
    }
}
