using Microsoft.AspNetCore.Mvc;
using MediatR;
using RealtimeNotifications.Functionality;
using RealtimeNotifications.Models;
using RealtimeNotifications.Functionality.Orders;
using RealtimeNotifications.Commands;

namespace RealtimeNotifications.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
        {
            var command = new UpdateOrderStatusCommand(id, newStatus);
            var success = await _mediator.Send(command);
            if (!success) return NotFound();
            return Ok();
        }

        //[HttpGet("notifications/unread/{userId}")]
        //public async Task<IActionResult> GetUnreadNotifications(string userId)
        //{
        //    var query = new GetUnreadNotificationsQuery(userId);
        //    var notifications = await _mediator.Send(query);
        //    return Ok(notifications);
        //}
        [HttpGet]
        public async Task<IActionResult> getOrders()
        {
            var command = new CreateOrderCommand();
            var success = await _mediator.Send(command);
            return Ok(success);
        }
    }
    
}
