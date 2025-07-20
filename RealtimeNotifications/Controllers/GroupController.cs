using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealtimeNotifications.Handlers.Group;
using System.Threading.Tasks;

namespace RealtimeNotifications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var command = new GroupCommand(userId);
            return Ok(await _mediator.Send(command));
        }
    }
}
