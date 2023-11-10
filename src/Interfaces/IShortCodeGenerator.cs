namespace Devblogs.Shortener.Interfaces;

public interface IShortCodeGenerator
{
    Task<string> GenerateAsync(string longUrl, CancellationToken cancellationToken);
}
