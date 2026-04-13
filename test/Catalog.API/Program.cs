using Asp.Versioning.Builder; // 导入API版本控制构建器命名空间，用于创建和配置API版本
using System.Reflection;        // 导入反射命名空间，提供类型信息访问功能

/// <summary>
/// 应用程序入口点类
/// </summary>
var builder = WebApplication.CreateBuilder(args); // 创建Web应用程序构建器，用于配置应用程序的服务和中间件

/// <summary>
/// 添加默认服务配置，包括健康检查、遥测等基础服务
/// </summary>
builder.AddServiceDefaults();

/// <summary>
/// 添加应用程序特定服务，如数据库上下文、业务服务等
/// </summary>
builder.AddApplicationServices();

/// <summary>
/// 添加问题详情服务，用于生成标准化的错误响应
/// </summary>
builder.Services.AddProblemDetails();

/// <summary>
/// 添加API版本控制服务，返回版本控制构建器
/// </summary>
var withApiVersioning = builder.Services.AddApiVersioning();

/// <summary>
/// 添加默认OpenAPI配置，用于生成API文档
/// </summary>
builder.AddDefaultOpenApi(withApiVersioning);

/// <summary>
/// 构建Web应用程序实例
/// </summary>
var app = builder.Build();

/// <summary>
/// 映射默认端点，如健康检查端点
/// </summary>
app.MapDefaultEndpoints();

/// <summary>
/// 使用状态码页面中间件，用于处理HTTP错误状态码
/// </summary>
app.UseStatusCodePages();

/// <summary>
/// 映射Catalog API端点，注册所有Catalog相关的API路由
/// </summary>
app.MapCatalogApi();

/// <summary>
/// 使用默认OpenAPI中间件，启用API文档界面
/// </summary>
app.UseDefaultOpenApi();

/// <summary>
/// 运行应用程序，启动HTTP服务器
/// </summary>
app.Run();
