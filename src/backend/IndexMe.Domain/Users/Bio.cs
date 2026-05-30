using IndexMe.Domain.Exceptions;

namespace IndexMe.Domain.Users;

public sealed record Bio
{
    private const int MaxLength = 250;
    public string? Value { get; init; }
    public Bio(string? value)
    {
        if (value is not null && value.Length > MaxLength)
        {
            throw new DomainValidationException($"BIO_CANNOT_BE_LONGER_THAN_{MaxLength}_CHARACTERS");
        }

        Value = value;
    }
}
