namespace Devblogs.Shortener.AppSettings;

public class UrlShortenerSettings
{
    public const string SectionName = "UrlShortener";

    public required string BaseServiceUrl { get; set; }

    public required int ShortCodeLength { get; set; }

    public required int DefaultExpirationCachedTagOnDays { get; set; } = 10;
}
