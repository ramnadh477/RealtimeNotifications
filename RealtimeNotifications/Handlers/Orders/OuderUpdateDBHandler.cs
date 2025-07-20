using Azure.Core;
using MediatR;
using RealtimeNotifications.Business;
using RealtimeNotifications.Commands;
using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;

namespace RealtimeNotifications.Handlers.Orders
{
    public class OuderUpdateDBHandler : INotificationHandler<OrderUpdateNotification>
    {
        private readonly INotificationRepository _context;
        public OuderUpdateDBHandler(INotificationRepository context)
        {
            _context = context;
        }
        public Task Handle(OrderUpdateNotification request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                UserId = request.UserId,
                Message = request.Message,
                IsRead = request.IsRead,
                Timestamp = request.Timestamp
            };
            _context.CreateAsync(notification);
            return Task.CompletedTask;
        }
    }
    
}
