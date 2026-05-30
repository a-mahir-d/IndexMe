using IndexMe.Domain.Results;
using Microsoft.Extensions.Logging;
using TS.MediatR;

namespace IndexMe.Application.Behaviors;

public sealed class ResultLoggingBehavior<TRequest, TResponse>(ILogger<ResultLoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();
        if (response is Result result && !string.IsNullOrWhiteSpace(result.LogMessage))
        {
            var requestName = typeof(TRequest).Name;
            if (result.LogMessage is not null)
            {
                if (result.IsSuccess)
                {
                    logger.LogInformation("[{RequestName}] İşlem Başarılı: {LogMessage}", requestName, result.LogMessage);
                }
                else
                {
                    logger.LogError("[{RequestName}] İşlem Başarısız. Hata: {Error} | Detay: {LogMessage}", requestName, result.Error, result.LogMessage);
                }
            }
        }

        return response;
    }
}
