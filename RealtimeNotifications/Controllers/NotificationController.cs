using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealtimeNotifications.Commands;
using RealtimeNotifications.Functionality;
using RealtimeNotifications.Handlers;

namespace RealtimeNotifications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderUpdateNotification command)
        {
            await _mediator.Publish(command);
            return Ok("Notification sent");
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var query = new getNotificationQuery(id);
            var not = await _mediator.Send(query);
            return Ok(not);
        }
        [HttpGet("{id}/{read}")]
        
        public async Task<IActionResult> Get(int id,bool read)
        {
            var query = new getreadNotificationQuery(id,read);
            var not = await _mediator.Send(query);
            return Ok(not);
        }
        [HttpPut]
        public async Task<IActionResult> Put(UpdateNotificationObj id) {
            var query = new updateNotificationQuery(id);
            var not = await _mediator.Send(query);
            
            return Ok();
        }
    }
}
