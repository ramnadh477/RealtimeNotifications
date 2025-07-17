// Features/Orders/UpdateOrderStatusCommand.cs
using MediatR;
using RealtimeNotifications.Interfaces;

public record UpdateOrderStatusCommand(int OrderId, string NewStatus) : IRequest<bool>;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly INotificationService _notificationService;

    public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository, INotificationService notificationService)
    {
        _orderRepository = orderRepository;
        _notificationService = notificationService;
    }

    public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order == null) return false;

        order.Status = request.NewStatus;
        await _orderRepository.UpdateAsync(order);

        var subscribedUsers = await _notificationService.GetUsersByGroupAsync("order-updates");
        foreach (var userId in subscribedUsers)
        {
            await _notificationService.SendNotificationAsync(userId, $"Order {order.Id} status updated to {order.Status}");
        }

        return true;
    }
}

