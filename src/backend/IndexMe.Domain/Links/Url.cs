namespace IndexMe.Domain.Links;

public sealed record Url
{
    private const int MaxLength = 2048;

    public string Value { get; init; }

    public Url(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("URL_CANNOT_BE_EMPTY");
        }

        string trimmedUrl = value.Trim();

        if (trimmedUrl.Length > MaxLength)
        {
            throw new ArgumentException($"URL_CANNOT_BE_LONGER_THAN_{MaxLength}_CHARACTERS");
        }

        if (!Uri.TryCreate(trimmedUrl, UriKind.Absolute, out Uri? uriResult) || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException("URL_INVALID_FORMAT_OR_SCHEME");
        }

        Value = uriResult.AbsoluteUri;
    }
}
