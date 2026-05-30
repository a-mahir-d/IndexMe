using IndexMe.Domain.Exceptions;
using IndexMe.Domain.Results;
using System.Reflection;
using TS.MediatR;

namespace IndexMe.Application.Behaviors;

public sealed class DomainExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public DomainExceptionBehavior() : base() { }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (DomainValidationException ex)
        {
            var responseType = typeof(TResponse);
            if (responseType == typeof(Result))
            {
                var failureResult = Result.Failure(ex.ErrorKey);
                return (TResponse)(object)failureResult;
            }

            if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var failureMethod = responseType.GetMethod(nameof(Result.Failure), BindingFlags.Public | BindingFlags.Static, [typeof(string), typeof(string)])
                    ?? throw new InvalidOperationException($"{responseType.Name} yapısında Failure metodu çözülemedi.");

                var genericFailureResult = failureMethod.Invoke(null, [ex.ErrorKey, null]);
                return (TResponse)genericFailureResult!;
            }

            throw;
        }
    }
}
