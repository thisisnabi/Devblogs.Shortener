namespace Devblogs.Shortener.Extensions;

public static class StringExtensions
{
    public static bool Found(this string? url)
        => !string.IsNullOrEmpty(url);
}
