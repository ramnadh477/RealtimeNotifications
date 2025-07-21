using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RealtimeNotifications.Handlers.Group;
using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;

namespace RealtimeNotifications.Hubs
{
    [Authorize]
    public class NotificationHub(IMediator mediator, INotificationRepository notrepo, ILogger<NotificationHub> logger) : Hub
    {
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await Clients.Caller.SendAsync("ReceiveConnectionId", connectionId);
            await base.OnConnectedAsync();
            logger.LogInformation($"Connected SignlR : {connectionId}");
        }

        public async Task SendNotification(string userId, string message)
        {
            string? isersd = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(isersd))
            {
                await Clients.User(isersd).SendAsync("ReceiveNotification", message);
            }
        }
        public async Task UpdateNotification(int Id, int userID, string message, bool IsRead, DateTime date)
        {
           await notrepo.UpdateStatus(Id, IsRead);
        }
        public async Task JoinGroup(UserGroupDto userGroup)
        {
            userGroup.Connectionid = Context.ConnectionId;
            var cmd = new GroupJoin(userGroup);
            await mediator.Publish(cmd);
            logger.LogInformation($"User {userGroup.UserName} joined Group {userGroup.GroupName}");
        }

        public async Task JoinGroups(List<string> GroupNames)
        {
            logger.LogInformation($"User joined Groups");
            foreach (var group in GroupNames)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, group);
            }
        }
        public async Task AddNotification(int Id, int userID, string message, bool IsRead, DateTime date)
        {
            var notification = new Notification
            {
                IsRead = IsRead,
                UserId = userID,
                Message = message,
                Timestamp = date
            };
            await notrepo.CreateAsync(notification);
        }
        
    }
}
