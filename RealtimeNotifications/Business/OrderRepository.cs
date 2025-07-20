using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;

namespace RealtimeNotifications.Business
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new()
        {
            new Order { Id = 1, customerName = "Alice Johnson", Product = "Laptop", Quantity = 1, Price = 1200.00m, Date = DateTime.Now.AddDays(-3),Status="Pending" },
            new Order { Id = 2, customerName = "Bob Smith", Product = "Smartphone", Quantity = 2, Price = 799.99m, Date = DateTime.Now.AddDays(-2),Status="Ordered" },
            new Order { Id = 3, customerName = "Charlie Lee", Product = "Monitor", Quantity = 1, Price = 250.00m, Date = DateTime.Now.AddDays(-1),Status="Confirmed" },
            new Order { Id = 4, customerName = "Diana Prince", Product = "Keyboard", Quantity = 3, Price = 75.00m, Date = DateTime.Now ,Status="Pending" },
            new Order { Id = 5, customerName = "Ethan Hunt", Product = "Mouse", Quantity = 2, Price = 40.00m, Date = DateTime.Now.AddDays(-5) ,Status="Pending" },
        };
public Task<Order> GetByIdAsync(int orderId)=>
            Task.FromResult(_orders.First(o => o.Id == orderId));
        public Task<List<Order>> GetOrderAsync() =>
            Task.FromResult(_orders);

        public Task UpdateAsync(Order order)
        {
            var existing = _orders.FirstOrDefault(o => o.Id == order.Id);
            if (existing != null) existing.Status = order.Status;
            return Task.CompletedTask;
        }
    }
}
