namespace Devblogs.Shortener.Interfaces;

public interface IUrlShortenerService
{
    Task<string> ShortenUrlAsync(string longUrl, CancellationToken cancellationToken);

    Task<string> GetLongUrlAsync(string shortCode, CancellationToken cancellationToken);
}
