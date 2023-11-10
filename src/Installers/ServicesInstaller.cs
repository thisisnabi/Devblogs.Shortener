namespace Devblogs.Services.Shortener.Installers;

public sealed class ServicesInstaller : IServiceCollectionInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUrlShortenerService, UrlShortenerService>();
        services.AddScoped<IShortCodeGenerator, ShortCodeGenerator>();
    }
}
