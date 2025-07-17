using RealtimeNotifications.Models;

namespace RealtimeNotifications.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetUnreadNotificationsAsync(string userId);
    }

}
