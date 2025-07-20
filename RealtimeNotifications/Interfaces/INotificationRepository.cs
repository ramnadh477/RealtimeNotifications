using RealtimeNotifications.Handlers;
using RealtimeNotifications.Models;

namespace RealtimeNotifications.Interfaces
{
    public interface INotificationRepository
    {
        Task CreateAsync(Notification notification);
        Task UpdateStatus(int Id, bool IsRead);
        Task<List<NotificationObj>> GetNotificationById(int Id);
        Task<List<getreaNotificationObj>> GetNotificationByIdandReadStatus(int Id,bool isRead);

        Task<List<UpdateNotificationObj>> Update(Notification notification);

    }

}
