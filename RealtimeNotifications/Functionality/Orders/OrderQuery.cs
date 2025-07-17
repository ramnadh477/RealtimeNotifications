using MediatR;
using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Commands;
using RealtimeNotifications.Models;

namespace RealtimeNotifications.Functionality.Orders
{

    public class OrderQueryHandler : IRequestHandler<CreateOrderCommand,List<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        public OrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetOrderAsync();
        }

    }
}
