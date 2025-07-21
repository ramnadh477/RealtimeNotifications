using MediatR;
using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;

namespace RealtimeNotifications.Handlers.Group
{
    public record GroupJoin(UserGroupDto id) : INotification;
    public class GrouopJoinCommandHandler(IGroupRepository _context, ILogger<GrouopJoinCommandHandler> _logger) : INotificationHandler<GroupJoin>
    {
        public async Task Handle(GroupJoin request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updated userGroup {request.id.GroupName} for user {request.id.UserName}");
            await _context.UpdateUserGroup(request.id);
        }
    }
}
