namespace Devblogs.Shortener.Installers;

public class DbContextInstaller : IServiceCollectionInstaller
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var conStr = configuration.GetConnectionString(ShortenerDbContext.ConnectionStringName);

        services.AddDbContext<ShortenerDbContext>(options =>
        {
            options.UseSqlServer(conStr);
        });

        services.AddScoped<ITagRepository, TagRepository>();  
    }
}
