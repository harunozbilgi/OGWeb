using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OGWeb.Core.Wrappers;
using System.Net;

namespace OGWeb.Core.Extensions;

public class ExceptionMiddleware
{
    public readonly RequestDelegate _request;
    public readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate request, ILogger<ExceptionMiddleware> logger)
    {
        _request = request;
        _logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await context.Response.WriteAsync(new ErrorResponse()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message ?? string.Empty,
        }.ToString());
    }
}
