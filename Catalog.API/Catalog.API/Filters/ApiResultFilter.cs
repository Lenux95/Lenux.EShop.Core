using Catalog.API.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.API.Filters;

public class ApiResultFilter : IAsyncResultFilter
{
    private readonly ILogger<ApiResultFilter> _logger;

    public ApiResultFilter(ILogger<ApiResultFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            var path = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;
            var value = objectResult.Value;
            var statusCode = objectResult.StatusCode;

            if (value is null)
            {
                _logger.LogDebug("响应包装: null值转为ApiResponse.Fail | {Method} {Path}", method, path);
                objectResult.Value = ApiResponse.Fail(statusCode ?? 500, "操作失败");
            }
            else if (!IsApiResponse(value))
            {
                //_logger.LogDebug("响应包装: {TypeName}自动包装为ApiResponse<> | {Method} {Path}",
                //    value.GetType().Name, method, path);
                objectResult.Value = ApiResponse<object>.Success(value);
            }
        }

        await next();
    }

    private static bool IsApiResponse(object value)
    {
        var type = value.GetType();
        if (type == typeof(ApiResponse))
            return true;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ApiResponse<>))
            return true;

        return false;
    }
}
