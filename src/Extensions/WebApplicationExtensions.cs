namespace Devblogs.Shortener.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplicationBuilder AddInstallers(this WebApplicationBuilder builder)
    {
        builder.Services.InstallFromAssembly<IShortenerAssemblyMarker>(builder.Configuration);
        return builder;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {

        app.MapPost("/shorten", async ([FromBody] ShortenRequest request,
                                              IUrlShortenerService urlShortenerService,
                                              CancellationToken cancellationToken) =>
                                        {
                                            var shortUrl = await urlShortenerService.ShortenUrlAsync(request.Url, cancellationToken);
                                            return Results.Ok(new { ShortUrl = shortUrl });
                                        }).AddEndpointFilter<ShortenEndpointFilter>();

        app.MapGet("{shortCode}", async ([FromRoute] string shortCode,
                                                IUrlShortenerService urlShortenerService,
                                                CancellationToken cancellationToken) =>
                                          {
                                              var longUrl = await urlShortenerService.GetLongUrlAsync(shortCode, cancellationToken);
                                          
                                              if (longUrl.Found())
                                              {
                                                  return Results.Redirect(longUrl!);
                                              }
                                          
                                              return Results.BadRequest(Constants.Data.EndPointFilterMessages.InvalidUrl);
                                          }).AddEndpointFilter<RedirectEndpointFilter>()
                                            .AllowAnonymous();

        return app;
    }
}
