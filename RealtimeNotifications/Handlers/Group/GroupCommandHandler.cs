using MediatR;
using RealtimeNotifications.Commands;
using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;

namespace RealtimeNotifications.Handlers.Group
{
    
    public record GroupCommand(int userID) : IRequest<List<GroupsDto>>;

    public class GroupCommandHandler(IGroupRepository _context, ILogger<GroupCommandHandler> _logger) : IRequestHandler<GroupCommand, List<GroupsDto>>
    {
        public Task<List<GroupsDto>> Handle(GroupCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"getting userGroups");
            return _context.GetAllGroups(request.userID);
        }
    }
}
