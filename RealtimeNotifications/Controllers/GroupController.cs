using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealtimeNotifications.Handlers.Group;
using RealtimeNotifications.Models;
using System.Threading.Tasks;

namespace RealtimeNotifications.Controllers
{
    public record GroupDto(string groupName);
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
         [HttpPost]
        public async Task<IActionResult> Post(GroupDto group)
        {
            if (!string.IsNullOrEmpty(group.groupName))
            {
                var command = new GroupCMD(group.groupName);
                await _mediator.Send(command);
                return Ok("Group Created");
            }
            else
                return BadRequest();
            
           
        } 
    }
}
