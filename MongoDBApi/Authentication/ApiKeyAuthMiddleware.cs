using System.Net;

namespace MongoDBApi.Authentication;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if(!context.Request.Headers.TryGetValue(AuthContains.ApiKeyHeaderName, 
               out var extractedApiKey))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("API Key missing!");
            return;
        }
        
        var apiKey = _configuration.GetValue<string>(AuthContains.ApiKeySectionName);
        if (apiKey != extractedApiKey)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Invalid API Key!");
            return;
        }
        
        await _next(context);
    }
}