using MediatR;
using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;
using RealtimeNotifications;

namespace RealtimeNotifications.Handlers.Group
{
    public record GroupCMD(string Group) : IRequest;
    public class GroupCreateHandler(IGroupRepository _context) : IRequestHandler<GroupCMD>
    {
        public Task Handle(GroupCMD request, CancellationToken cancellationToken)
        {
            var group = new RealtimeNotifications.Group { GroupName = request.Group };
            return Task.FromResult(_context.CreatGroup(group));
        }
    }
}
