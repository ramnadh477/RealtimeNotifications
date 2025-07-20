using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealtimeNotifications.Commands;
using RealtimeNotifications.Hubs;

namespace RealtimeNotifications.Handlers.Orders
{
    public class OrderUpdateSignlrHandler : INotificationHandler<OrderUpdateNotification>
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<OrderUpdateSignlrHandler> _logger;
        public OrderUpdateSignlrHandler(IHubContext<NotificationHub> hubContext, ILogger<OrderUpdateSignlrHandler> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task Handle(OrderUpdateNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Notification sent to user");
            await _hubContext.Clients.User(notification.UserId.ToString())
                .SendAsync("ReceiveNotification",notification.Message);
            _logger.LogInformation($"Notification sent to  order-updates Group");
            await _hubContext.Clients.Group("order-updates").SendAsync("ReceiveGroupNotification", $"Message has changed {notification.Message}.");
            
        }
    }
}
