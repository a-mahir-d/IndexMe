using IndexMe.Domain.Exceptions;

namespace IndexMe.Domain.Users;

public sealed record DisplayName
{
    private const int MaxLength = 50;
    public string? Value { get; init; }
    public DisplayName(string? value)
    {
        if (value is not null && value.Length > MaxLength)
        {
            throw new DomainValidationException($"DISPLAY_NAME_CANNOT_BE_LONGER_THAN_{MaxLength}_CHARACTERS");
        }

        Value = value;
    }
}