﻿namespace Devblogs.Services.Shortener.Installers;

public sealed class CacheInstaller : IServiceCollectionInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache(mem =>
        {
            var settings = configuration.GetSection(UrlShortenerSettings.SectionName).Get<UrlShortenerSettings>();
             
            if (settings == null) 
            {
                throw new ArgumentNullException(nameof(UrlShortenerSettings));  
            }

            mem.ExpirationScanFrequency = TimeSpan.FromDays(settings.DefaultExpirationCachedOnDays);
        });
    }
}
