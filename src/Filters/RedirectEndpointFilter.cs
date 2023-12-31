﻿namespace Devblogs.Shortener.Filters;

public class RedirectEndpointFilter(IOptions<UrlShortenerSettings> settingOption) : IEndpointFilter
{
    private readonly UrlShortenerSettings _setting = settingOption.Value;
    private const int UrlArgumentIndex = 0;
    private const string ValidArgumentRegexPattern = @"^[a-zA-Z0-9-]+$";

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argumentToken = context.GetArgument<string>(UrlArgumentIndex);

        var urlIsValid = IsTokenArgumentValid(argumentToken);
        
        if (urlIsValid)
        {
            return await next(context);
        }

        return Results.BadRequest(Constants.Data.EndPointFilterMessages.InvalidUrl);
    }

    private bool IsTokenArgumentValid(string tokenArgument)
    {
        if(string.IsNullOrWhiteSpace(tokenArgument))
            return false;
        
        if (_setting.ShortCodeLength != tokenArgument.Length)
            return false;

        if (!Regex.IsMatch(tokenArgument, ValidArgumentRegexPattern))
            return false;

        return true;
    }
}