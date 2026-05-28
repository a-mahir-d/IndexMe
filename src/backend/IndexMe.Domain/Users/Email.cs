using System.Text.RegularExpressions;

namespace IndexMe.Domain.Users;

public sealed record Email
{
    private static readonly Regex EmailPattern = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private const int MaxLength = 254;
    public string Value { get; init; }
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("EMAIL_CANNOT_BE_EMPTY");
        }

        string trimmedEmail = value.Trim().ToLowerInvariant();

        if (trimmedEmail.Length > MaxLength)
        {
            throw new ArgumentException($"EMAIL_CANNOT_BE_LONGER_THAN_{MaxLength}_CHARACTERS");
        }

        if (!EmailPattern.IsMatch(trimmedEmail))
        {
            throw new ArgumentException("EMAIL_INVALID_FORMAT");
        }

        Value = value;
    }
}