using System.Net;
using System.Text.Json;
using Serilog;

namespace Catalog.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // 在中间件中排除健康检查等路径
        if (context.Request.Path.StartsWithSegments("/health"))
        {
            await _next(context);
            return;
        }

        _logger.LogError(exception,
            "全局异常捕获 - Path: {Path}, Method: {Method}",
            context.Request.Path,
            context.Request.Method);

        var response = context.Response;
        response.ContentType = "application/json";

        var (statusCode, message, details) = exception switch
        {
            ArgumentException => (HttpStatusCode.BadRequest, exception.Message, null),
            KeyNotFoundException => (HttpStatusCode.NotFound, exception.Message, null),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "未授权访问", null),
            InvalidOperationException => (HttpStatusCode.BadRequest, "操作无效", null),
            _ => (HttpStatusCode.InternalServerError, "服务器内部错误",
                  _environment.IsDevelopment() ? exception.StackTrace : null)
        };

        response.StatusCode = (int)statusCode;

        var result = new
        {
            code = (int)statusCode,
            message,
            details,
            timestamp = DateTime.UtcNow
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await response.WriteAsync(JsonSerializer.Serialize(result, jsonOptions));
    }
}
