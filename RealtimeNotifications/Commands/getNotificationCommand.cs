using MediatR;

namespace RealtimeNotifications.Commands
{
    public class getNotificationCommand : IRequest
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
