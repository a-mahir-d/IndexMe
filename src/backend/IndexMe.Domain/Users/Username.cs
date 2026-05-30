using IndexMe.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace IndexMe.Domain.Users;

public sealed record Username
{
    private static readonly Regex UsernamePattern = new(@"^[a-z0-9_]+$", RegexOptions.Compiled);
    private const int MaxLength = 254;
    public string Value { get; init; }
    public Username(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new DomainValidationException("USERNAME_CANNOT_BE_EMPTY");
        }

        if (value.Length < 6)
        {
            throw new DomainValidationException("USERNAME_CANNOT_BE_SHORTER_THEN_6_CHARACTERS");
        }

        if (value.Length > MaxLength)
        {
            throw new DomainValidationException($"USERNAME_CANNOT_BE_LONGER_THAN_{MaxLength}_CHARACTERS");
        }

        if (!UsernamePattern.IsMatch(value))
        {
            throw new DomainValidationException("USERNAME_CAN_ONLY_CONTAIN_LOWERCASE_LETTERS_NUMBERS_AND_UNDERSCORES");
        }

        Value = value;
    }
}