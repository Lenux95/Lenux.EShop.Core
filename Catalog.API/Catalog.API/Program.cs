using System.Net.Http.Headers;
using Catalog.API.Extensions;
using Catalog.API.Filters;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiResultFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCatalogServices(builder.Configuration);

builder.Services.AddPythonServices(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .MinimumLevel.Information()
        // 过滤第三方库的警告
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Serilog.AspNetCore", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "Catalog.API")
        .ReadFrom.Configuration(context.Configuration);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 全局异常处理中间件
app.UseMiddleware<Catalog.API.Middleware.ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// 注册Serilog请求日志中间件
app.UseSerilogRequestLogging(options =>
{
    // 添加额外的诊断上下文
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
    };
});

// 在请求开始时添加上下文
app.Use(async (context, next) =>
{
    using (LogContext.PushProperty("ClientIp", context.Connection.RemoteIpAddress))
    {
        await next();
    }
});


app.UseAuthorization();

app.MapControllers();

app.Run();
