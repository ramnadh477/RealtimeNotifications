using RealtimeNotifications.Models;

namespace RealtimeNotifications.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int orderId);
        public Task<List<Order>> GetOrderAsync();
        Task UpdateAsync(Order order);
    }
}
