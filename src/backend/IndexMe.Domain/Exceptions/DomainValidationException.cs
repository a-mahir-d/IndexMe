namespace IndexMe.Domain.Exceptions;

public sealed class DomainValidationException(string errorKey) : Exception(errorKey)
{
    public string ErrorKey { get; } = errorKey;
}
