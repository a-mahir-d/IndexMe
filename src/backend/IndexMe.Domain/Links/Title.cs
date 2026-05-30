using IndexMe.Domain.Exceptions;

namespace IndexMe.Domain.Links;

public sealed record Title
{
    private const int MaxLength = 50;
    public string Value { get; init; }
    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new DomainValidationException($"TITLE_CANNOT_BE_EMPTY");
        if (value.Length > MaxLength) throw new DomainValidationException($"TITLE_CANNOT_BE_LONGER_THAN_{MaxLength}_CHARACTERS");

        Value = value;
    }
}
