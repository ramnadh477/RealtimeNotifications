using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using RealtimeNotifications.Commands;
namespace Client.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IMediator _mediator;
    public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async void OnGet()
    {
        //await _mediator.Send(new SendNotificationCommand
        //{
        //    UserId = "user-123",
        //    Message = "Your order has been shipped!"
        //});
    }
}
