using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using RealtimeNotifications.Commands;
using RealtimeNotifications.Interfaces;
using RealtimeNotifications.Models;
using System.Collections.Generic;

namespace RealtimeNotifications.Handlers
{
    public record NotificationObj(int Id, int UserId, string? Message, bool? IsRead, DateTime? Timestamp);
    public record getNotificationQuery(int id):IRequest<List<NotificationObj>>;
    public class GetNotificationHandler(INotificationRepository _context) : IRequestHandler<getNotificationQuery,List<NotificationObj>>
    {
        public Task<List<NotificationObj>> Handle(getNotificationQuery request, CancellationToken cancellationToken)
        {
            var result =_context.GetNotificationById(request.id);
            return result;
        }
    }

    public record getreaNotificationObj(int Id, int UserId, string? Message, bool? IsRead, DateTime? Timestamp);
    public record getreadNotificationQuery(int id,bool read) : IRequest<List<getreaNotificationObj>>;
    public class GetreadNotificationHandler(INotificationRepository _context) : IRequestHandler<getreadNotificationQuery, List<getreaNotificationObj>>
    {
        public  Task<List<getreaNotificationObj>> Handle(getreadNotificationQuery request, CancellationToken cancellationToken)
        {
            var result = _context.GetNotificationByIdandReadStatus(request.id, request.read);
            return result;
        }
    }

    public record UpdateNotificationObj(int Id, int UserId, string? Message, bool? IsRead, DateTime? Timestamp);
    public record updateNotificationQuery(UpdateNotificationObj Id) : IRequest<List<UpdateNotificationObj>>;
    public class UpdateNotificationHandler(INotificationRepository _context) : IRequestHandler<updateNotificationQuery, List<UpdateNotificationObj>>
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
            List<UpdateNotificationObj> result= await _context.Update(notification);
            return result;
            
        }
    }
}
