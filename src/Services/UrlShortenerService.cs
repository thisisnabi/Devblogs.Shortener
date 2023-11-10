
namespace Devblogs.Shortener.Services;

public sealed class UrlShortenerService(
        IOptions<UrlShortenerSettings> shortenerSettingOptions,
        ITagRepository tagRepository,
        IMemoryCache cache,
        IShortCodeGenerator shortCodeGenerator) : IUrlShortenerService
{
    private readonly UrlShortenerSettings _shortenerSetting = shortenerSettingOptions.Value;
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly IMemoryCache _cache = cache;
    private readonly IShortCodeGenerator _shortCodeGenerator = shortCodeGenerator;

    public async Task<string> ShortenUrlAsync(string longUrl, CancellationToken cancellationToken)
    {
        var shortCode = await _tagRepository.GetShortCodeAsync(longUrl, cancellationToken);
        if (shortCode.Found())
        {
            return GenerateUrl(shortCode!);
        }

        string generatedCode = await _shortCodeGenerator.GenerateAsync(longUrl, cancellationToken);

        var tag = Tag.Create(generatedCode, longUrl);
        await _tagRepository.InsertAsync(tag, cancellationToken);

        return GenerateUrl(generatedCode);
    }

    public async Task<string> GetLongUrlAsync(string shortCode, CancellationToken cancellationToken)
    {
        var longUrl = await _tagRepository.GetLongUrlAsync(shortCode, cancellationToken);
        if (longUrl.Found())
        {
            return longUrl!;
        }

        throw new NullReferenceException(nameof(longUrl));
    }
 
    private string GenerateUrl(string shortCode)
        => $"{_shortenerSetting.BaseUrl}/{shortCode}";
}