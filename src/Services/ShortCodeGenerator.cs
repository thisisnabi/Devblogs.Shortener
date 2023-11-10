namespace Devblogs.Shortener.Services;

public class ShortCodeGenerator(ITagRepository tagRepository, IOptions<UrlShortenerSettings> shortenerSettingOptions) : IShortCodeGenerator
{
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly UrlShortenerSettings _shortenerSetting = shortenerSettingOptions.Value;

    public async Task<string> GenerateAsync(string longUrl, CancellationToken cancellationToken)
    {
        using MD5 md5 = MD5.Create();

        byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(longUrl));
        string hashCode = BitConverter.ToString(hashBytes)
                                      .Replace(oldValue: "-", newValue: "")
                                      .ToLower();

        for (int i = 0; i <= hashCode.Length - _shortenerSetting.ShortCodeLength; i++)
        {
            string candidateCode = hashCode.Substring(i, _shortenerSetting.ShortCodeLength);
            var hasCode = await _tagRepository.IsShortCodeUsedAsync(candidateCode, cancellationToken);

            if (!hasCode)
            {
                return candidateCode;
            }
        }

        throw new Exception(Constants.Data.ExceptionMessage.FailedGenerateUniqueCode);
    }
}
