using MediatR;
using Microsoft.AspNetCore.SignalR;
using RealtimeNotifications.Hubs;
using RealtimeNotifications.Interfaces;

namespace RealtimeNotifications.Functionality
{
    //public record NotificationDto(int Id, string? Message, bool? IsRead, DateTime? CreatedAt);

    //public record GetUnreadNotificationsQuery(string UserId) : IRequest<List<NotificationDto>>;

    //public class GetUnreadNotificationsQueryHandler : IRequestHandler<GetUnreadNotificationsQuery, List<NotificationDto>>
    //{
    //    private readonly INotificationRepository _notificationRepository;
    //    private readonly IHubContext<NotificationHub> _hubContext;

    //    public GetUnreadNotificationsQueryHandler(INotificationRepository notificationRepository, IHubContext<NotificationHub> hubContext)
    //    {
    //        _notificationRepository = notificationRepository;
    //        _hubContext = hubContext;
    //    }

    //    public async Task<List<NotificationDto>> Handle(GetUnreadNotificationsQuery request, CancellationToken cancellationToken)
    //    {
    //        var unreadNotifications = await _notificationRepository.GetUnreadNotificationsAsync(request.UserId);
    //        await _hubContext.Clients.All
    //      .SendAsync("ReceiveNotification", unreadNotifications.Select(n => new NotificationDto(n.Id, n.Message, n.IsRead, n.Timestamp)).ToList(), cancellationToken);
    //        return unreadNotifications.Select(n => new NotificationDto(n.Id, n.Message, n.IsRead, n.Timestamp)).ToList();
    //    }
    //}

}
