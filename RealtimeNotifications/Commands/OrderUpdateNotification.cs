using MediatR;

namespace RealtimeNotifications.Commands
{
    public class OrderUpdateNotification : INotification
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string? Message { get; set; }

        public bool? IsRead { get; set; }

        public DateTime? Timestamp { get; set; }
    }
}
