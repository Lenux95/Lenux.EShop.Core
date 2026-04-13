using eShop.Catalog.API.Services; // 导入服务命名空间
using Microsoft.AspNetCore.Mvc; // 导入MVC命名空间，用于FromServices属性

/// <summary>
/// Catalog服务集合，包含所有依赖服务的注入
/// </summary>
public class CatalogServices(
    CatalogContext context, // 数据库上下文
    [FromServices] ICatalogAI catalogAI, // AI服务，使用FromServices标记从服务容器注入
    IOptions<CatalogOptions> options, // 配置选项
    ILogger<CatalogServices> logger, // 日志记录器
    [FromServices] ICatalogIntegrationEventService eventService) // 事件服务，使用FromServices标记从服务容器注入
{
    /// <summary>
    /// 数据库上下文，用于数据库操作
    /// </summary>
    public CatalogContext Context { get; } = context;

    /// <summary>
    /// AI服务，用于语义搜索
    /// </summary>
    public ICatalogAI CatalogAI { get; } = catalogAI;

    /// <summary>
    /// 配置选项，用于获取应用配置
    /// </summary>
    public IOptions<CatalogOptions> Options { get; } = options;

    /// <summary>
    /// 日志记录器，用于记录日志
    /// </summary>
    public ILogger<CatalogServices> Logger { get; } = logger;

    /// <summary>
    /// 事件服务，用于处理集成事件
    /// </summary>
    public ICatalogIntegrationEventService EventService { get; } = eventService;
};
