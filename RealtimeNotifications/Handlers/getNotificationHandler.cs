using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using RealtimeNotifications.Commands;

namespace RealtimeNotifications.Handlers
{
    public record NotificationObj(int Id, int UserId, string Message, bool? IsRead, DateTime? Timestamp);
    public record getNotificationQuery(int id):IRequest<List<NotificationObj>>;
    public class GetNotificationHandler(NotificationContext _context) : IRequestHandler<getNotificationQuery,List<NotificationObj>>
    {
        public async Task<List<NotificationObj>> Handle(getNotificationQuery request, CancellationToken cancellationToken)
        {
            return _context.Notifications.Where(n => n.UserId == request.id).Select(n => new NotificationObj(n.Id, n.UserId, n.Message, n.IsRead, n.Timestamp)).ToList();
        }
    }

    public record getreaNotificationObj(int Id, int UserId, string Message, bool? IsRead, DateTime? Timestamp);
    public record getreadNotificationQuery(int id,bool read) : IRequest<List<getreaNotificationObj>>;
    public class GetreadNotificationHandler(NotificationContext _context) : IRequestHandler<getreadNotificationQuery, List<getreaNotificationObj>>
    {
        public async Task<List<getreaNotificationObj>> Handle(getreadNotificationQuery request, CancellationToken cancellationToken)
        {
            return _context.Notifications.Where(n => n.UserId == request.id && n.IsRead==request.read).Select(n => new getreaNotificationObj(n.Id, n.UserId, n.Message, n.IsRead, n.Timestamp)).ToList();
        }
    }

    public record UpdateNotificationObj(int Id, int UserId, string Message, bool? IsRead, DateTime? Timestamp);
    public record updateNotificationQuery(UpdateNotificationObj Id) : IRequest<List<UpdateNotificationObj>>;
    public class UpdateNotificationHandler(NotificationContext _context) : IRequestHandler<updateNotificationQuery, List<UpdateNotificationObj>>
    {
        public async Task<List<UpdateNotificationObj>> Handle(updateNotificationQuery request, CancellationToken cancellationToken)
        {

            var notification = new Notification
            {
                Id = request.Id.Id,
                UserId = request.Id.UserId,
                Message = request.Id.Message,
                IsRead = request.Id.IsRead,
                Timestamp = request.Id.Timestamp
            };
            _context.Notifications.Update(notification);
            _context.SaveChanges();
            return _context.Notifications.Where(n => n.Id == request.Id.Id).Select(n => new UpdateNotificationObj(n.Id, n.UserId, n.Message, n.IsRead, n.Timestamp)).ToList();
        }
    }
}
