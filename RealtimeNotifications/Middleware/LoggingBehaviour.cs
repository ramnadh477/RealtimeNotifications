using MediatR;

namespace RealtimeNotifications.Middleware
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling {RequestName} with payload: {@Request}",
                typeof(TRequest).Name, request);

            try
            {
                var response = await next(cancellationToken);
                logger.LogInformation("Handled {RequestName}", typeof(TRequest).Name);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception in {RequestName}", typeof(TRequest).Name);
                throw;
            }
        }
    }

}
