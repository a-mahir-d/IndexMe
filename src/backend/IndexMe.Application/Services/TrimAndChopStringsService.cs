using System.Reflection;

namespace IndexMe.Application.Services;

internal sealed class TrimAndChopStringsService
{
    private const int MaxLength = 300;
    public static void TrimAndChop(object request)
    {
        if (request is null) return;

        var type = request.GetType();
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (property.GetMethod is null) continue;
            if (property.SetMethod is null || !property.SetMethod.IsPublic) continue;

            var value = property.GetValue(request);
            if (value is null) continue;

            if (value is string v)
            {
                v = v.Trim();
                if (v.Length > MaxLength) v = v[..MaxLength];
                property.SetValue(request, v);
            }
        }
    }
}
