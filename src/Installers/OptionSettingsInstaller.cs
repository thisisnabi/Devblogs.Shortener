namespace Devblogs.Services.Shortener.Installers;

public sealed class OptionSettingsInstaller : IServiceCollectionInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<UrlShortenerSettings>(configuration.GetSection(UrlShortenerSettings.SectionName));
    }
}
