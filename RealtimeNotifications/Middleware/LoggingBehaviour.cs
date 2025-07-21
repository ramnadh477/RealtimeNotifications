using MediatR;

namespace RealtimeNotifications.Middleware
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {RequestName} with payload: {@Request}",
                typeof(TRequest).Name, request);

            try
            {
                var response = await next();
                _logger.LogInformation("Handled {RequestName}", typeof(TRequest).Name);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in {RequestName}", typeof(TRequest).Name);
                throw;
            }
        }
    }

}
