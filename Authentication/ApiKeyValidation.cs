using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Library_API.Authentication
{
    public class ApiKeyValidation
    {
        private readonly RequestDelegate _next;
    private const string APIKEY = "X-API-KEY";

    public ApiKeyValidation(RequestDelegate next)
    {
        _next = next;
    }

        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
        {
                context.Response.StatusCode = 401;
                var result = JsonSerializer.Serialize(new { message = "API Key was not provided." });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
                return;
         }

        // validate the extracted API key
        var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = appSettings.GetValue<string>(APIKEY);

        if (!apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401;
                var result = JsonSerializer.Serialize(new { message = "Unauthorised Client" });
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
                return;
            }

        await _next(context);
           
    }
    }
}
