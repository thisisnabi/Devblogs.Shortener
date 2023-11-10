namespace Devblogs.Shortener.AppSettings;

public class UrlShortenerSettings
{
    public const string SectionName = "UrlShortener";

    public required string BaseUrl { get; set; }

    public required int ShortCodeLength { get; set; }

    public required int DefaultExpirationCachedOnDays { get; set; } = 10;
}
