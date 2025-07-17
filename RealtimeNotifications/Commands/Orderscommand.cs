using MediatR;
using RealtimeNotifications.Models;

namespace RealtimeNotifications.Commands
{
    public class CreateOrderCommand : IRequest<List<Order>>
    {
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
