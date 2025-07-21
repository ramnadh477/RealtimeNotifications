using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealtimeNotifications.Commands;
using RealtimeNotifications.Hubs;

namespace RealtimeNotifications.Handlers.Orders
{
    public class OrderUpdateSignlrHandler(IHubContext<NotificationHub> hubContext, ILogger<OrderUpdateSignlrHandler> logger) : INotificationHandler<OrderUpdateNotification>
    {
        public async Task Handle(OrderUpdateNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Notification sent to user");
            await hubContext.Clients.User(notification.UserId.ToString())
                .SendAsync("ReceiveNotification", notification.Message, cancellationToken: cancellationToken);
            logger.LogInformation($"Notification sent to  order-updates Group");
            await hubContext.Clients.Group("order-updates").SendAsync("ReceiveGroupNotification", $"Message has changed {notification.Message}.", cancellationToken: cancellationToken);
            
        }
    }
}
