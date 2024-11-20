using System.Text.Json;
using MediatR;

namespace FileStore.Api.DJ;

public class RequestResponseLoggingBehavior<TRequest, TResponse>(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var correlationId = Guid.NewGuid();
        
        var requestJson = JsonSerializer.Serialize(request);
        
        logger.LogInformation($"Handling request {correlationId}: {requestJson}");
        
        var response = await next();
        
        // var responseJson = JsonSerializer.Serialize(response);
        //
        // logger.LogInformation($"Response response {correlationId}: {responseJson}");

        return response;
    }
}