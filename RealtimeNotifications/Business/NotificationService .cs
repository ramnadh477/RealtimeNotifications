using RealtimeNotifications.Interfaces;

namespace RealtimeNotifications.Business
{
    public class NotificationService : INotificationService
    {
        private readonly Dictionary<string, List<int>> _groups = new()
    {
        { "order-updates", new List<int> { 101, 102, 103 } }
    };
        public Task<List<int>> GetUsersByGroupAsync(string groupName)
        {
            _groups.TryGetValue(groupName, out var users);
            return Task.FromResult(users ?? new List<int>());
        }

        public Task SendNotificationAsync(int userId, string message)
        {
            Console.WriteLine($"Notification sent to User {userId}: {message}");
            return Task.CompletedTask;
        }
    }
}
