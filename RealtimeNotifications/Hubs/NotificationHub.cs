using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace RealtimeNotifications.Hubs
{
    [Authorize]
    public class NotificationHub(NotificationContext _context) : Hub
    {
        
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
        public async Task SendNotification(string userId, string message)
        {
            var isersd = Context.UserIdentifier;
            await Clients.User(isersd).SendAsync("ReceiveNotification", message);
            //await Clients.All.SendAsync("ReceiveNotification", message);
            await Clients.Group("order-updates").SendAsync("ReceiveNotification", message);
            
        }
        public  async Task UpdateNotification(int Id,int userID,string message, bool IsRead,DateTime date)
        {
            var notification = new Notification
            {
                Id = Id,
                IsRead = IsRead,
                UserId=userID,
                Message=message,
                Timestamp=date
            };
             _context.Notifications.Update(notification);
             _context.SaveChanges();
             await Clients.All.SendAsync("ReceiveNotification1", "");
        }
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            
            await Clients.Group(groupName).SendAsync("ReceiveNotification", $"{Context.UserIdentifier} has joined the group {groupName}.");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}
