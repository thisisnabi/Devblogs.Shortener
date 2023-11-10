namespace Devblogs.Shortener.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplicationBuilder InstallExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.InstallFromAssembly<IShortenerAssemblyMarker>(builder.Configuration);
        return builder;
    }
}
