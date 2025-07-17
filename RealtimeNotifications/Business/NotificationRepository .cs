using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;

namespace RealtimeNotifications.Business
{
    public class NotificationRepository 
    {
    //    private readonly List<Notification> _notifications = new()
    //{
    //    new Notification { Id = 1, UserId = "101", Message = "Order #1 shipped", IsRead = false, Timestamp = DateTime.UtcNow.AddHours(-1) },
    //    new Notification { Id = 2, UserId = "101", Message = "Order #2 delivered", IsRead = true, Timestamp = DateTime.UtcNow.AddDays(-1) },
    //    new Notification { Id = 3, UserId = "102", Message = "Order #3 shipped", IsRead = false, Timestamp = DateTime.UtcNow.AddMinutes(-30) }
    //};
    //    public Task<List<Notification>> GetUnreadNotificationsAsync(string userId)
    //    {
    //        var unread = _notifications.Where(n => n.UserId == userId && !n.IsRead).ToList();
    //        return Task.FromResult(unread);
    //    }
    }
}
