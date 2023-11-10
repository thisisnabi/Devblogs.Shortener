namespace Devblogs.Shortener.Interfaces;

public interface ITagRepository
{
    Task InsertAsync(Tag tag, CancellationToken cancellationToken);

    Task<string?> GetLongUrlAsync(string shortCode, CancellationToken cancellationToken);

    Task<string?> GetShortCodeAsync(string longUrl, CancellationToken cancellationToken);
 
    Task<bool> IsShortCodeUsedAsync(string candidateCode, CancellationToken cancellationToken);
}
