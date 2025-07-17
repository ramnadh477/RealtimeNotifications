using MediatR;
using Microsoft.AspNetCore.SignalR;
using RealtimeNotifications.Hubs;
using RealtimeNotifications.Commands;

namespace RealtimeNotifications.Handlers
{
    
   

    public class SendNotificationHandler : IRequestHandler<SendNotificationCommand>
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly NotificationContext _context;

        public SendNotificationHandler(IHubContext<NotificationHub> hubContext, NotificationContext notificationContext)
        {
            _hubContext = hubContext;
            _context = notificationContext;
        }

        public async Task<Unit> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                
                UserId = request.UserId,
                Message=request.Message,
                IsRead = request.IsRead,
                Timestamp = request.Timestamp
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();
          //  await _hubContext.Clients.All
          //.SendAsync("ReceiveNotification", request.Message, cancellationToken);

            return Unit.Value;
        }

        Task IRequestHandler<SendNotificationCommand>.Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }

}
