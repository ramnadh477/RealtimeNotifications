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
        
        [HttpGet]
        public async Task<IActionResult> getOrders()
        {
            var command = new CreateOrderCommand();
            var success = await _mediator.Send(command);
            return Ok(success);
        }
    }
    
}
