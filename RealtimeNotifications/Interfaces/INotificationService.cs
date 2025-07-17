namespace RealtimeNotifications.Interfaces
{
    public interface INotificationService
    {
        Task<List<int>> GetUsersByGroupAsync(string groupName);
        Task SendNotificationAsync(int userId, string message);
    }
}
