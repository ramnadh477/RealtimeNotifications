using RealtimeNotifications.Models;

namespace RealtimeNotifications.Interfaces
{
    public interface INotificationService
    {
        Task CreateAsync(Notification notification);
    }
}
