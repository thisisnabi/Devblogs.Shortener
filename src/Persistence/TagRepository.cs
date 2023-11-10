namespace Devblogs.Shortener.Data;

public class TagRepository(ShortenerDbContext shortenerDbContext, IMemoryCache memoryCache) : ITagRepository
{
    private readonly ShortenerDbContext _shortenerDbContext = shortenerDbContext;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task InsertAsync(Tag tag, CancellationToken cancellationToken)
    {
        await _shortenerDbContext.Tags.AddAsync(tag, cancellationToken);
        _memoryCache.Set(tag.ShortCode, tag.LongUrl);

        await _shortenerDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<string?> GetLongUrlAsync(string shortCode, CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(shortCode,out string? longUrl))
        {
            return longUrl;
        }
         
        var tag = await _shortenerDbContext.Tags.FirstOrDefaultAsync(x => x.ShortCode == shortCode, cancellationToken);
        return tag?.LongUrl;
    }

    public async Task<string?> GetShortCodeAsync(string longUrl, CancellationToken cancellationToken)
    {
        var tag = await _shortenerDbContext.Tags.FirstOrDefaultAsync(x => x.LongUrl == longUrl, cancellationToken);
        
        return tag?.ShortCode;
    }
       
    public async Task<bool> IsShortCodeUsedAsync(string candidateCode, CancellationToken cancellationToken)
        => !string.IsNullOrEmpty(await GetLongUrlAsync(candidateCode, cancellationToken));
}
