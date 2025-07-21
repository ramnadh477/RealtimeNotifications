using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RealtimeNotifications.Hubs;
using System.Text.RegularExpressions;

namespace RealtimeNotifications.Handlers.Group
{
    public class GroupJoinSignlRHandler(IHubContext<NotificationHub> hubContext,ILogger<GroupJoinSignlRHandler> _logger) : INotificationHandler<GroupJoin>
    {
        public async Task Handle(GroupJoin request, CancellationToken cancellationToken)
        {
            var groupname = request.id.GroupName;
            if (!string.IsNullOrWhiteSpace(groupname))
            {
                if (request.id.isJoined)
                {
                   await hubContext.Clients.Group(groupname).SendAsync("ReceiveNotification", $"{request.id.UserName} has Left the group {groupname}.", cancellationToken: cancellationToken);
                   await hubContext.Groups.RemoveFromGroupAsync(request.id.Connectionid.ToString(), groupname, cancellationToken);
                    _logger.LogInformation($"User {request.id.UserName} left the Group {request.id.GroupName}");
                }
                else
                {
                   await hubContext.Groups.AddToGroupAsync(request.id.Connectionid.ToString(), groupname, cancellationToken);
                   await hubContext.Clients.Group(groupname).SendAsync("ReceiveNotification", $"{request.id.UserName} has joined the group {groupname}.", cancellationToken: cancellationToken);
                    _logger.LogInformation($"User {request.id.UserName} Joined the Group {request.id.GroupName}");
                }
            }
                
        }
    }
}
